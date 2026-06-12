using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using BitSortCore;

namespace BitSortApp
{
    public partial class Form1 : Form
    {
        private List<CleanTarget> targets;
        private Dictionary<string, CheckedListBox> tabsMap = new();
        
        private Button btnPurge;
        private Button btnAbout;
        private RichTextBox txtLogConsole;
        private Label lblInfo;

        public Form1()
        {
            this.Text = "BitSort v2.2";
            this.Size = new Size(920, 600);
            this.BackColor = Color.FromArgb(28, 28, 28);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.ico");
            if (!File.Exists(iconPath)) 
            {
                iconPath = Path.Combine(Directory.GetCurrentDirectory(), "BitSortApp", "app.ico");
            }

            if (File.Exists(iconPath))
            {
                try 
                { 
                    Icon customIcon = new Icon(iconPath);
                    this.Icon = customIcon; 
                } 
                catch { }
            }

            targets = BitSortEngine.GetTargets();
            BuildProfessionalUi();
        }

        private void BuildProfessionalUi()
        {

            Label lblMainTitle = new() { Text = "BitSort", Location = new Point(25, 20), Size = new Size(300, 30), Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.LightGray };
            this.Controls.Add(lblMainTitle);

            btnAbout = new Button {
                Text = "ℹ About App",
                Location = new Point(325, 18), Size = new Size(100, 30),
                BackColor = Color.FromArgb(55, 55, 55), ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnAbout.FlatAppearance.BorderSize = 0;
            btnAbout.Click += BtnAbout_Click;
            this.Controls.Add(btnAbout);
            btnAbout.BringToFront(); // Принудительно выводим кнопку на передний план

            // TabControl
            TabControl tabControl = new() { Location = new Point(25, 65), Size = new Size(400, 260) };
            this.Controls.Add(tabControl);

            string[][] cats = { 
                new[] { "Dev", "💻 Dev Caches" }, 
                new[] { "System", "System Junk" }, 
                new[] { "Drivers", "Hardware Drivers" }
            };

            foreach (var cat in cats)
            {
                TabPage page = new() { Text = cat[1], BackColor = Color.FromArgb(35, 35, 35) };
                
                CheckedListBox clb = new() { 
                    Location = new Point(10, 10), Size = new Size(375, 210), 
                    BackColor = Color.FromArgb(45, 45, 45), ForeColor = Color.White, 
                    BorderStyle = BorderStyle.None, CheckOnClick = true 
                };
                
                clb.SelectedIndexChanged += (s, e) => {
                    var c = (CheckedListBox)s!;
                    string name = c.SelectedItem?.ToString() ?? "";
                    var t = targets.FirstOrDefault(x => x.Name == name);
                    if (t != null) lblInfo.Text = t.Description;
                };

                var items = targets.Where(x => x.Category == cat[0]);
                foreach (var item in items) clb.Items.Add(item.Name, item.Selected);

                page.Controls.Add(clb);
                tabControl.TabPages.Add(page);
                tabsMap.Add(cat[0], clb);
            }

            Label lblDescTag = new() { Text = "DESCRIPTION:", Location = new Point(25, 345), Size = new Size(200, 20), Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Gray };
            this.Controls.Add(lblDescTag);

            lblInfo = new Label { Location = new Point(25, 370), Size = new Size(390, 80), ForeColor = Color.FromArgb(200, 200, 200), Font = new Font("Segoe UI", 9.5f, FontStyle.Regular) };
            this.Controls.Add(lblInfo);
            lblInfo.Text = targets[0].Description;

            btnPurge = new Button { 
                Text = "▶ START OPTIMIZATION", 
                Location = new Point(25, 480), Size = new Size(400, 50), 
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold) 
            };
            btnPurge.FlatAppearance.BorderSize = 0;
            btnPurge.Click += BtnPurge_Click;
            this.Controls.Add(btnPurge);

            // Terminal
            Label lblConsoleTag = new() { Text = "EXECUTION LOGS", Location = new Point(460, 45), Size = new Size(200, 20), Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Gray };
            this.Controls.Add(lblConsoleTag);

            txtLogConsole = new RichTextBox { 
                Location = new Point(460, 65), Size = new Size(425, 465), 
                BackColor = Color.FromArgb(20, 20, 20), ForeColor = Color.LightGray, 
                BorderStyle = BorderStyle.None, ReadOnly = true, Font = new Font("Consolas", 9.5f) 
            };
            this.Controls.Add(txtLogConsole);

            txtLogConsole.AppendText("[Status]: System integration active. Ready.\n");
        }

        private void BtnAbout_Click(object? sender, EventArgs e)
        {
            Form aboutForm = new Form {
                Text = "Program Passport",
                Size = new Size(400, 280),
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.White,
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                StartPosition = FormStartPosition.CenterParent,
                Icon = this.Icon // Применяем ту же иконку и к этому окну
            };

            Label lblInfoText = new Label {
                Location = new Point(20, 20),
                Size = new Size(350, 220),
                Font = new Font("Segoe UI", 10),
                Text = "🌀 BITSORT UTILITY\n\n" +
                       "▪ Product: System & Dev Cache Optimizer\n" +
                       "▪ Version: 2.2 Stable (2026)\n" +
                       "▪ Developer: Muchprow_IT\n\n" +
                       "Description:\n" +
                       "A specialized utility engineered to safely purge heavy modern software caches (Docker, Gradle, NuGet, npm, pip) and manage third-party system hardware driver packages via native PnP subsystems."
            };

            aboutForm.Controls.Add(lblInfoText);
            aboutForm.ShowDialog(this);
        }

        private void BtnPurge_Click(object? sender, EventArgs e)
        {
            foreach (var pair in tabsMap)
            {
                var clb = pair.Value;
                for (int i = 0; i < clb.Items.Count; i++)
                {
                    string name = clb.Items[i].ToString()!;
                    var t = targets.First(x => x.Name == name);
                    t.Selected = clb.GetItemChecked(i);
                }
            }

            btnPurge.Enabled = false;
            btnPurge.Text = "Processing targets...";
            btnPurge.BackColor = Color.FromArgb(60, 60, 60);
            txtLogConsole.Clear();
            this.Update();

            string logs = BitSortEngine.ExecutePurge(targets);

            txtLogConsole.AppendText(logs);
            txtLogConsole.SelectionStart = txtLogConsole.Text.Length;
            txtLogConsole.ScrollToCaret();

            btnPurge.Enabled = true;
            btnPurge.Text = "▶ START OPTIMIZATION";
            btnPurge.BackColor = Color.FromArgb(0, 122, 204);
        }
    }
}