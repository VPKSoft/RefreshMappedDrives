#region License
/*
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>
*/
#endregion

using System;
using System.Windows.Forms;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading;

namespace RefreshMappedDrives
{
    public partial class FormMain : Form
    {
        private int TryCountSeconds { get; }

        private int CurrentCountMilliseconds { get; set; }

        public FormMain()
        {
            InitializeComponent();
            foreach (var arg in Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("--tryCount", StringComparison.InvariantCultureIgnoreCase))
                {
                    try
                    {
                        TryCountSeconds = int.Parse(arg.Split('=')[0]);
                    }
                    catch
                    {
                        TryCountSeconds = 10;
                    }
                }
            }
        }

        private void UpdateItemsToOk()
        {
            foreach (ListViewItem item in lvDrives.Items)
            {
                item.ImageIndex = 1;
                item.SubItems[3].Text = true.ToString();
            }
        }

        private ListViewItem GetExistingItem(string drive)
        {
            foreach (ListViewItem item in lvDrives.Items)
            {
                if (item.SubItems[1].Text == drive)
                {
                    return item;
                }
            }

            return null;
        }

        private bool ItemOk(ListViewItem item)
        {
            return bool.Parse(item.SubItems[3].Text);
        }


        private bool AllOk
        {
            get
            {
                bool result = true;
                foreach (ListViewItem item in lvDrives.Items)
                {
                    result &= bool.Parse(item.SubItems[3].Text);
                }

                return result;
            }
        }

        private void RefreshDrives()
        {
            try
            {
                UpdateItemsToOk();
                using (Runspace runSpace = RunspaceFactory.CreateRunspace(RunspaceConfiguration.Create()))
                {
                    runSpace.Open();

                    using (Pipeline pipeline = runSpace.CreatePipeline())
                    {

                        pipeline.Commands.AddScript(
                            "Get-SmbMapping | where -property Status -Value Unavailable -EQ | select LocalPath,RemotePath");

                        var results = pipeline.Invoke();

                        foreach (var result in results)
                        {
                            var listViewItem = GetExistingItem(result.Properties["LocalPath"].Value.ToString()) == null
                                ? new ListViewItem()
                                : GetExistingItem(result.Properties["LocalPath"].Value.ToString());

                            if (listViewItem.SubItems.Count > 1)
                            {
                                // handled already..
                                continue;
                            }

                            listViewItem.ImageIndex = 0;
                            listViewItem.SubItems.Clear();
                            listViewItem.SubItems.Add(result.Properties["LocalPath"].Value.ToString());
                            listViewItem.SubItems.Add(result.Properties["RemotePath"].Value.ToString());
                            listViewItem.SubItems.Add(false.ToString());
                            lvDrives.Items.Add(listViewItem);
                        }
                    }

                    runSpace.Close();

                    if (AllOk)
                    {
                        AllowClose = true;
                        Close();
                    }
                }
            }
            catch
            {
                // ignored..
            }
        }

        private void UpdateMappedDrives()
        {
            try
            {
                using (var powerShell = PowerShell.Create())
                {
                    foreach (ListViewItem item in lvDrives.Items)
                    {
                        if (ItemOk(item))
                        {
                            continue;
                        }

                        var script =
                            $"New-SmbMapping -LocalPath {item.SubItems[1].Text} -RemotePath {item.SubItems[2].Text} -Persistent $True";

                        powerShell.Commands.AddScript(script);
                    }

                    powerShell.Invoke();
                }

                RefreshDrives();

                if (AllOk)
                {
                    AllowClose = true;
                    Close();
                }
            }
            catch
            {
                // ignored..
            }
        }


        private void TmRefresh_Tick(object sender, EventArgs e)
        {
            tmRefresh.Enabled = false;

            if (TryCountSeconds > CurrentCountMilliseconds / 1000)
            {
                AllowClose = true;
                Close();
                return;
            }

            UpdateMappedDrives();

            CurrentCountMilliseconds += tmRefresh.Interval;

            tmRefresh.Enabled = true;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            RefreshDrives();
            tmRefresh.Enabled = true;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
                Hide();
                return;
            }
            tmRefresh.Enabled = false;
            Thread.Sleep(300);
            RefreshWindowsExplorer();
        }

        private bool AllowClose { get; set; }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        /// <summary>
        /// (C):: https://stackoverflow.com/questions/2488727/refresh-windows-explorer-in-win7
        /// </summary>
        private static void RefreshWindowsExplorer()
        {
            try
            {
                // based on http://stackoverflow.com/questions/2488727/refresh-windows-explorer-in-win7
                Guid clsIdShellApplication = new Guid("13709620-C279-11CE-A49E-444553540000");
                Type shellApplicationType = Type.GetTypeFromCLSID(clsIdShellApplication, true);

                object shellApplication = Activator.CreateInstance(shellApplicationType);
                object windows = shellApplicationType.InvokeMember("Windows",
                    System.Reflection.BindingFlags.InvokeMethod, null, shellApplication, new object[] { });

                Type windowsType = windows.GetType();
                object count = windowsType.InvokeMember("Count", System.Reflection.BindingFlags.GetProperty, null,
                    windows, null);
                for (int i = 0; i < (int) count; i++)
                {
                    object item = windowsType.InvokeMember("Item", System.Reflection.BindingFlags.InvokeMethod, null,
                        windows, new object[] {i});
                    Type itemType = item.GetType();

                    // only refresh windows explorers
                    string itemName = (string) itemType.InvokeMember("Name", System.Reflection.BindingFlags.GetProperty,
                        null, item, null);

                    if (itemName == "Windows Explorer" || itemName == "File Explorer")
                    {
                        itemType.InvokeMember("Refresh", System.Reflection.BindingFlags.InvokeMethod, null, item, null);
                    }
                }
            }
            catch
            {
                // ignored..
            }
        }
    }
}
