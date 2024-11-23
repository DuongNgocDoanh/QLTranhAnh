namespace QLTranhAnh
{
    partial class Form2
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
            this.panel_Top = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_Left = new System.Windows.Forms.Panel();
            this.btDanhMuc = new System.Windows.Forms.Button();
            this.btnHoadon = new System.Windows.Forms.Button();
            this.btnTimkiem = new System.Windows.Forms.Button();
            this.btnKhachhang = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.panel_Body = new System.Windows.Forms.Panel();
            this.panel_Top.SuspendLayout();
            this.panel_Left.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Top
            // 
            this.panel_Top.Controls.Add(this.label1);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(200, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(632, 100);
            this.panel_Top.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tranh Ảnh";
            // 
            // panel_Left
            // 
            this.panel_Left.Controls.Add(this.btDanhMuc);
            this.panel_Left.Controls.Add(this.btnHoadon);
            this.panel_Left.Controls.Add(this.btnTimkiem);
            this.panel_Left.Controls.Add(this.btnKhachhang);
            this.panel_Left.Controls.Add(this.btnThoat);
            this.panel_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Left.Location = new System.Drawing.Point(0, 0);
            this.panel_Left.Name = "panel_Left";
            this.panel_Left.Size = new System.Drawing.Size(200, 676);
            this.panel_Left.TabIndex = 3;
            // 
            // btDanhMuc
            // 
            this.btDanhMuc.Location = new System.Drawing.Point(28, 100);
            this.btDanhMuc.Name = "btDanhMuc";
            this.btDanhMuc.Size = new System.Drawing.Size(144, 55);
            this.btDanhMuc.TabIndex = 5;
            this.btDanhMuc.Text = "Danh Mục";
            this.btDanhMuc.UseVisualStyleBackColor = true;
            this.btDanhMuc.Click += new System.EventHandler(this.btDanhMuc_Click);
            // 
            // btnHoadon
            // 
            this.btnHoadon.Location = new System.Drawing.Point(28, 176);
            this.btnHoadon.Name = "btnHoadon";
            this.btnHoadon.Size = new System.Drawing.Size(144, 54);
            this.btnHoadon.TabIndex = 4;
            this.btnHoadon.Text = "Hoá Đơn";
            this.btnHoadon.UseVisualStyleBackColor = true;
            this.btnHoadon.Click += new System.EventHandler(this.btnHoadon_Click_1);
            // 
            // btnTimkiem
            // 
            this.btnTimkiem.Location = new System.Drawing.Point(28, 268);
            this.btnTimkiem.Name = "btnTimkiem";
            this.btnTimkiem.Size = new System.Drawing.Size(144, 58);
            this.btnTimkiem.TabIndex = 3;
            this.btnTimkiem.Text = "Tìm Kiếm";
            this.btnTimkiem.UseVisualStyleBackColor = true;
            this.btnTimkiem.Click += new System.EventHandler(this.btnTimkiem_Click_1);
            // 
            // btnKhachhang
            // 
            this.btnKhachhang.Location = new System.Drawing.Point(28, 370);
            this.btnKhachhang.Name = "btnKhachhang";
            this.btnKhachhang.Size = new System.Drawing.Size(144, 59);
            this.btnKhachhang.TabIndex = 1;
            this.btnKhachhang.Text = "Khách Hàng";
            this.btnKhachhang.UseVisualStyleBackColor = true;
            this.btnKhachhang.Click += new System.EventHandler(this.btnKhachhang_Click_1);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(28, 514);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(144, 54);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // panel_Body
            // 
            this.panel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Body.Location = new System.Drawing.Point(200, 100);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(632, 576);
            this.panel_Body.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 676);
            this.Controls.Add(this.panel_Body);
            this.Controls.Add(this.panel_Top);
            this.Controls.Add(this.panel_Left);
            this.Name = "Form2";
            this.Text = "Form2";
            this.panel_Top.ResumeLayout(false);
            this.panel_Top.PerformLayout();
            this.panel_Left.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_Left;
        private System.Windows.Forms.Button btDanhMuc;
        private System.Windows.Forms.Button btnHoadon;
        private System.Windows.Forms.Button btnTimkiem;
        private System.Windows.Forms.Button btnKhachhang;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Panel panel_Body;
    }
}