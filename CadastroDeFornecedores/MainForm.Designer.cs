namespace WindowsFormsApp1
{
    partial class MainForm
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
            this.dataGridViewFornecedores = new System.Windows.Forms.DataGridView();
            this.btnEditar = new System.Windows.Forms.Button();
            this.bntNovo = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnBuscarCNPJ = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFornecedores)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewFornecedores
            // 
            this.dataGridViewFornecedores.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewFornecedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFornecedores.Location = new System.Drawing.Point(12, 64);
            this.dataGridViewFornecedores.Name = "dataGridViewFornecedores";
            this.dataGridViewFornecedores.Size = new System.Drawing.Size(1010, 226);
            this.dataGridViewFornecedores.TabIndex = 0;
            // 
            // btnEditar
            // 
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.Location = new System.Drawing.Point(13, 297);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(150, 60);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "EDITAR";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // bntNovo
            // 
            this.bntNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntNovo.Location = new System.Drawing.Point(169, 296);
            this.bntNovo.Name = "bntNovo";
            this.bntNovo.Size = new System.Drawing.Size(150, 60);
            this.bntNovo.TabIndex = 1;
            this.bntNovo.Text = "CADASTRAR";
            this.bntNovo.UseVisualStyleBackColor = true;
            this.bntNovo.Click += new System.EventHandler(this.bntNovo_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.Location = new System.Drawing.Point(13, 363);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(150, 60);
            this.btnExcluir.TabIndex = 1;
            this.btnExcluir.Text = "EXLCUIR";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnBuscarCNPJ
            // 
            this.btnBuscarCNPJ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarCNPJ.Location = new System.Drawing.Point(702, 474);
            this.btnBuscarCNPJ.Name = "btnBuscarCNPJ";
            this.btnBuscarCNPJ.Size = new System.Drawing.Size(320, 60);
            this.btnBuscarCNPJ.TabIndex = 1;
            this.btnBuscarCNPJ.Text = "BUSCAR NOVO CNPJ";
            this.btnBuscarCNPJ.UseVisualStyleBackColor = true;
            this.btnBuscarCNPJ.Click += new System.EventHandler(this.btnBuscarCNPJ_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lista dos meu Fornecedores:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 546);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnBuscarCNPJ);
            this.Controls.Add(this.bntNovo);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.dataGridViewFornecedores);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastrar Fornecedores";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFornecedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFornecedores;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button bntNovo;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnBuscarCNPJ;
        private System.Windows.Forms.Label label1;
    }
}