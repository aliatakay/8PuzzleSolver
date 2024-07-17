namespace App
{
    partial class PuzzleForm
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
            this.lblTimeText = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblMoveText = new System.Windows.Forms.Label();
            this.lblMove = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_last = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.lblMoves = new System.Windows.Forms.ListBox();
            this.cbSolution = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cbSpeed = new System.Windows.Forms.ComboBox();
            this.lblSpeedText = new System.Windows.Forms.Label();
            this.lblSecond = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTimeText
            // 
            this.lblTimeText.AutoSize = true;
            this.lblTimeText.Font = new System.Drawing.Font("Arial", 15F);
            this.lblTimeText.Location = new System.Drawing.Point(288, 475);
            this.lblTimeText.Name = "lblTimeText";
            this.lblTimeText.Size = new System.Drawing.Size(58, 23);
            this.lblTimeText.TabIndex = 0;
            this.lblTimeText.Text = "Time:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Arial", 15F);
            this.lblTime.ForeColor = System.Drawing.Color.Red;
            this.lblTime.Location = new System.Drawing.Point(365, 475);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(21, 23);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "0";
            // 
            // lblMoveText
            // 
            this.lblMoveText.AutoSize = true;
            this.lblMoveText.Font = new System.Drawing.Font("Arial", 15F);
            this.lblMoveText.Location = new System.Drawing.Point(288, 441);
            this.lblMoveText.Name = "lblMoveText";
            this.lblMoveText.Size = new System.Drawing.Size(64, 23);
            this.lblMoveText.TabIndex = 0;
            this.lblMoveText.Text = "Move:";
            // 
            // lblMove
            // 
            this.lblMove.AutoSize = true;
            this.lblMove.Font = new System.Drawing.Font("Arial", 15F);
            this.lblMove.ForeColor = System.Drawing.Color.Red;
            this.lblMove.Location = new System.Drawing.Point(365, 441);
            this.lblMove.Name = "lblMove";
            this.lblMove.Size = new System.Drawing.Size(21, 23);
            this.lblMove.TabIndex = 0;
            this.lblMove.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(5, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 347);
            this.panel1.TabIndex = 20;
            // 
            // cb_last
            // 
            this.cb_last.AutoSize = true;
            this.cb_last.Checked = true;
            this.cb_last.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_last.Font = new System.Drawing.Font("Arial", 15F);
            this.cb_last.Location = new System.Drawing.Point(34, 471);
            this.cb_last.Name = "cb_last";
            this.cb_last.Size = new System.Drawing.Size(198, 27);
            this.cb_last.TabIndex = 26;
            this.cb_last.Text = "Last Square Empty";
            this.cb_last.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.OrangeRed;
            this.btnNew.Font = new System.Drawing.Font("Arial", 25F);
            this.btnNew.Location = new System.Drawing.Point(34, 354);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(230, 58);
            this.btnNew.TabIndex = 28;
            this.btnNew.Text = "New Game";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSolve
            // 
            this.btnSolve.BackColor = System.Drawing.Color.IndianRed;
            this.btnSolve.Font = new System.Drawing.Font("Arial", 25F);
            this.btnSolve.Location = new System.Drawing.Point(287, 354);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(227, 58);
            this.btnSolve.TabIndex = 29;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = false;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // lblMoves
            // 
            this.lblMoves.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMoves.FormattingEnabled = true;
            this.lblMoves.ItemHeight = 38;
            this.lblMoves.Location = new System.Drawing.Point(549, 104);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(115, 308);
            this.lblMoves.TabIndex = 31;
            // 
            // cbSolution
            // 
            this.cbSolution.AutoSize = true;
            this.cbSolution.Checked = true;
            this.cbSolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSolution.Font = new System.Drawing.Font("Arial", 15F);
            this.cbSolution.Location = new System.Drawing.Point(34, 437);
            this.cbSolution.Name = "cbSolution";
            this.cbSolution.Size = new System.Drawing.Size(186, 27);
            this.cbSolution.TabIndex = 33;
            this.cbSolution.Text = "Show the Solution";
            this.cbSolution.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Arial", 22F);
            this.lblTitle.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblTitle.Location = new System.Drawing.Point(530, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(154, 75);
            this.lblTitle.TabIndex = 34;
            this.lblTitle.Text = "Optimum Solution";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbSpeed
            // 
            this.cbSpeed.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbSpeed.FormattingEnabled = true;
            this.cbSpeed.Items.AddRange(new object[] {
            "0.05",
            "0.10",
            "0.25",
            "0.50",
            "1.00"});
            this.cbSpeed.Location = new System.Drawing.Point(572, 438);
            this.cbSpeed.Name = "cbSpeed";
            this.cbSpeed.Size = new System.Drawing.Size(66, 31);
            this.cbSpeed.TabIndex = 35;
            // 
            // lblSpeedText
            // 
            this.lblSpeedText.AutoSize = true;
            this.lblSpeedText.Font = new System.Drawing.Font("Arial", 15F);
            this.lblSpeedText.Location = new System.Drawing.Point(493, 441);
            this.lblSpeedText.Name = "lblSpeedText";
            this.lblSpeedText.Size = new System.Drawing.Size(73, 23);
            this.lblSpeedText.TabIndex = 36;
            this.lblSpeedText.Text = "Speed:";
            // 
            // lblSecond
            // 
            this.lblSecond.AutoSize = true;
            this.lblSecond.Font = new System.Drawing.Font("Arial", 15F);
            this.lblSecond.Location = new System.Drawing.Point(644, 441);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(41, 23);
            this.lblSecond.TabIndex = 37;
            this.lblSecond.Text = "sec";
            // 
            // PuzzleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 510);
            this.Controls.Add(this.lblSecond);
            this.Controls.Add(this.lblSpeedText);
            this.Controls.Add(this.cbSpeed);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbSolution);
            this.Controls.Add(this.lblMoves);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.cb_last);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblMove);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblMoveText);
            this.Controls.Add(this.lblTimeText);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.MaximizeBox = false;
            this.Name = "PuzzleForm";
            this.Text = "Puzzle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTimeText;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblMoveText;
        private System.Windows.Forms.Label lblMove;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cb_last;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.ListBox lblMoves;
        private System.Windows.Forms.CheckBox cbSolution;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cbSpeed;
        private System.Windows.Forms.Label lblSpeedText;
        private System.Windows.Forms.Label lblSecond;
    }
}