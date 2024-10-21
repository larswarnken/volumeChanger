namespace volumeChanger
{
    partial class Form1
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
            button1 = new Button();
            label1 = new Label();
            panelBrd1 = new Panel();
            panelVolBlue = new Panel();
            panelVolWhite = new Panel();
            panelBrd1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(366, 385);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(380, 28);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // panelBrd1
            // 
            panelBrd1.BackColor = SystemColors.ControlDark;
            panelBrd1.Controls.Add(panelVolWhite);
            panelBrd1.Controls.Add(panelVolBlue);
            panelBrd1.Location = new Point(200, 100);
            panelBrd1.Name = "panelBrd1";
            panelBrd1.Size = new Size(29, 204);
            panelBrd1.TabIndex = 3;
            // 
            // panelVolBlue
            // 
            panelVolBlue.BackColor = SystemColors.GradientInactiveCaption;
            panelVolBlue.Location = new Point(2, 2);
            panelVolBlue.Name = "panelVolBlue";
            panelVolBlue.Size = new Size(25, 200);
            panelVolBlue.TabIndex = 4;
            // 
            // panelVolWhite
            // 
            panelVolWhite.Anchor = AnchorStyles.Bottom;
            panelVolWhite.BackColor = SystemColors.Control;
            panelVolWhite.Location = new Point(2, 2);
            panelVolWhite.Name = "panelVolWhite";
            panelVolWhite.Size = new Size(25, 150);
            panelVolWhite.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 450);
            Controls.Add(panelBrd1);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            panelBrd1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Label label1;
        private Panel panelBrd1;
        private Panel panelVolBlue;
        private Panel panelVolWhite;
    }
}
