using System.Xml.Linq;

namespace WACCA_Config
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Start = new Button();
            SelectPath = new Button();
            ServerIP = new TextBox();
            ServerIPLabel = new Label();
            SelectPathLabel = new Label();
            SuspendLayout();
            // 
            // Start
            // 
            Start.BackColor = SystemColors.ControlDarkDark;
            Start.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            Start.Location = new Point(182, 325);
            Start.Name = "Start";
            Start.Size = new Size(172, 31);
            Start.TabIndex = 0;
            Start.Text = "Start WACCA Reverse";
            Start.UseVisualStyleBackColor = false;
            Start.Click += Start_Click;
            // 
            // SelectPath
            // 
            SelectPath.BackColor = SystemColors.ControlDarkDark;
            SelectPath.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            SelectPath.Location = new Point(12, 12);
            SelectPath.Name = "SelectPath";
            SelectPath.Size = new Size(172, 31);
            SelectPath.TabIndex = 1;
            SelectPath.Text = "Select WACCA Path";
            SelectPath.UseVisualStyleBackColor = false;
            SelectPath.Click += SelectPath_Click;
            // 
            // ServerIP
            // 
            ServerIP.BackColor = SystemColors.ControlDarkDark;
            ServerIP.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            ServerIP.ForeColor = SystemColors.ControlLightLight;
            ServerIP.Location = new Point(182, 49);
            ServerIP.Name = "ServerIP";
            ServerIP.Size = new Size(172, 25);
            ServerIP.TabIndex = 2;
            ServerIP.TextChanged += ServerIP_TextChanged;
            // 
            // ServerIPLabel
            // 
            ServerIPLabel.BackColor = Color.Gray;
            ServerIPLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            ServerIPLabel.Location = new Point(104, 49);
            ServerIPLabel.Name = "ServerIPLabel";
            ServerIPLabel.Size = new Size(72, 25);
            ServerIPLabel.TabIndex = 3;
            ServerIPLabel.Text = "ServerIP:";
            // 
            // SelectPathLabel
            // 
            SelectPathLabel.BackColor = Color.FromArgb(155, 105, 105, 105);
            SelectPathLabel.Font = new Font("Segoe UI", 25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectPathLabel.Location = new Point(12, 1000);
            SelectPathLabel.Name = "SelectPathLabel";
            SelectPathLabel.Size = new Size(495, 310);
            SelectPathLabel.TabIndex = 4;
            SelectPathLabel.Text = "Please select the path to WACCA before doing anything!";
            SelectPathLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            ClientSize = new Size(519, 361);
            Controls.Add(SelectPathLabel);
            Controls.Add(ServerIPLabel);
            Controls.Add(ServerIP);
            Controls.Add(SelectPath);
            Controls.Add(Start);
            ForeColor = SystemColors.ControlLightLight;
            MaximizeBox = false;
            MaximumSize = new Size(535, 400);
            MinimizeBox = false;
            MinimumSize = new Size(535, 400);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WACCA Config";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Start;
        private Button SelectPath;
        private TextBox ServerIP;
        private Label ServerIPLabel;
        private Label SelectPathLabel;
    }
}