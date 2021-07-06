namespace TankBattle
{
    partial class GameplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameplayForm));
            this.displayPanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.FireButton = new System.Windows.Forms.Button();
            this.PowerSelector = new System.Windows.Forms.TrackBar();
            this.AngleSelector = new System.Windows.Forms.NumericUpDown();
            this.WeaponSelector = new System.Windows.Forms.ComboBox();
            this.Power = new System.Windows.Forms.Label();
            this.PowerLabel = new System.Windows.Forms.Label();
            this.AngleLabel = new System.Windows.Forms.Label();
            this.Weapon = new System.Windows.Forms.Label();
            this.WindSpeedDirection = new System.Windows.Forms.Label();
            this.windLabel = new System.Windows.Forms.Label();
            this.CurrentName = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PowerSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(0, 32);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(800, 600);
            this.displayPanel.TabIndex = 0;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.controlPanel.Controls.Add(this.FireButton);
            this.controlPanel.Controls.Add(this.PowerSelector);
            this.controlPanel.Controls.Add(this.AngleSelector);
            this.controlPanel.Controls.Add(this.WeaponSelector);
            this.controlPanel.Controls.Add(this.Power);
            this.controlPanel.Controls.Add(this.PowerLabel);
            this.controlPanel.Controls.Add(this.AngleLabel);
            this.controlPanel.Controls.Add(this.Weapon);
            this.controlPanel.Controls.Add(this.WindSpeedDirection);
            this.controlPanel.Controls.Add(this.windLabel);
            this.controlPanel.Controls.Add(this.CurrentName);
            this.controlPanel.Enabled = false;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(800, 32);
            this.controlPanel.TabIndex = 1;
            this.controlPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.controlPanel_Paint);
            // 
            // FireButton
            // 
            this.FireButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FireButton.Location = new System.Drawing.Point(713, 3);
            this.FireButton.Name = "FireButton";
            this.FireButton.Size = new System.Drawing.Size(75, 23);
            this.FireButton.TabIndex = 10;
            this.FireButton.Text = "Fire!";
            this.FireButton.UseVisualStyleBackColor = true;
            this.FireButton.Click += new System.EventHandler(this.FireButton_Click);
            // 
            // PowerSelector
            // 
            this.PowerSelector.LargeChange = 10;
            this.PowerSelector.Location = new System.Drawing.Point(513, 0);
            this.PowerSelector.Maximum = 100;
            this.PowerSelector.Minimum = 5;
            this.PowerSelector.Name = "PowerSelector";
            this.PowerSelector.Size = new System.Drawing.Size(142, 45);
            this.PowerSelector.TabIndex = 9;
            this.PowerSelector.Value = 5;
            this.PowerSelector.Scroll += new System.EventHandler(this.PowerSelector_Scroll);
            // 
            // AngleSelector
            // 
            this.AngleSelector.Location = new System.Drawing.Point(398, 7);
            this.AngleSelector.Name = "AngleSelector";
            this.AngleSelector.Size = new System.Drawing.Size(46, 20);
            this.AngleSelector.TabIndex = 8;
            this.AngleSelector.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // WeaponSelector
            // 
            this.WeaponSelector.FormattingEnabled = true;
            this.WeaponSelector.Location = new System.Drawing.Point(213, 6);
            this.WeaponSelector.Name = "WeaponSelector";
            this.WeaponSelector.Size = new System.Drawing.Size(121, 21);
            this.WeaponSelector.TabIndex = 7;
            this.WeaponSelector.SelectedIndexChanged += new System.EventHandler(this.WeaponSelector_SelectedIndexChanged);
            // 
            // Power
            // 
            this.Power.AutoSize = true;
            this.Power.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Power.Location = new System.Drawing.Point(661, 7);
            this.Power.Name = "Power";
            this.Power.Size = new System.Drawing.Size(27, 19);
            this.Power.TabIndex = 6;
            this.Power.Text = "25";
            // 
            // PowerLabel
            // 
            this.PowerLabel.AutoSize = true;
            this.PowerLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PowerLabel.Location = new System.Drawing.Point(450, 9);
            this.PowerLabel.Name = "PowerLabel";
            this.PowerLabel.Size = new System.Drawing.Size(57, 18);
            this.PowerLabel.TabIndex = 5;
            this.PowerLabel.Text = "Power:";
            this.PowerLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // AngleLabel
            // 
            this.AngleLabel.AutoSize = true;
            this.AngleLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AngleLabel.Location = new System.Drawing.Point(340, 8);
            this.AngleLabel.Name = "AngleLabel";
            this.AngleLabel.Size = new System.Drawing.Size(52, 18);
            this.AngleLabel.TabIndex = 4;
            this.AngleLabel.Text = "Angle:";
            this.AngleLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // Weapon
            // 
            this.Weapon.AutoSize = true;
            this.Weapon.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Weapon.Location = new System.Drawing.Point(136, 8);
            this.Weapon.Name = "Weapon";
            this.Weapon.Size = new System.Drawing.Size(71, 18);
            this.Weapon.TabIndex = 3;
            this.Weapon.Text = "Weapon:";
            // 
            // WindSpeedDirection
            // 
            this.WindSpeedDirection.AutoSize = true;
            this.WindSpeedDirection.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WindSpeedDirection.Location = new System.Drawing.Point(96, 15);
            this.WindSpeedDirection.Name = "WindSpeedDirection";
            this.WindSpeedDirection.Size = new System.Drawing.Size(13, 14);
            this.WindSpeedDirection.TabIndex = 2;
            this.WindSpeedDirection.Text = "0";
            this.WindSpeedDirection.Click += new System.EventHandler(this.WindSpeedDirection_Click);
            // 
            // windLabel
            // 
            this.windLabel.AutoSize = true;
            this.windLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windLabel.Location = new System.Drawing.Point(96, 1);
            this.windLabel.Name = "windLabel";
            this.windLabel.Size = new System.Drawing.Size(34, 14);
            this.windLabel.TabIndex = 1;
            this.windLabel.Text = "Wind";
            this.windLabel.Click += new System.EventHandler(this.windLabel_Click);
            // 
            // CurrentName
            // 
            this.CurrentName.AutoSize = true;
            this.CurrentName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentName.Location = new System.Drawing.Point(3, 3);
            this.CurrentName.Name = "CurrentName";
            this.CurrentName.Size = new System.Drawing.Size(72, 24);
            this.CurrentName.TabIndex = 0;
            this.CurrentName.Text = "Player";
            this.CurrentName.Click += new System.EventHandler(this.label1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GameplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 629);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.displayPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "GameplayForm";
            this.Text = "Form1";
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PowerSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleSelector)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label CurrentName;
        private System.Windows.Forms.Label windLabel;
        private System.Windows.Forms.Label WindSpeedDirection;
        private System.Windows.Forms.Label PowerLabel;
        private System.Windows.Forms.Label AngleLabel;
        private System.Windows.Forms.Label Weapon;
        private System.Windows.Forms.Button FireButton;
        private System.Windows.Forms.TrackBar PowerSelector;
        private System.Windows.Forms.NumericUpDown AngleSelector;
        private System.Windows.Forms.ComboBox WeaponSelector;
        private System.Windows.Forms.Label Power;
    }
}

