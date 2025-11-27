using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormRegistroPersonal : Form
    {
        private Sistema sistema;

        public FormRegistroPersonal(Sistema sistemaParam)
        {
            sistema = sistemaParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(800, 700);
            this.Text = "Registro de Personal (Administrador General)";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Registro de Personal Hospitalario";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(200, 20);
            lblTitulo.Size = new Size(400, 40);
            this.Controls.Add(lblTitulo);

            Label lblInfo = new Label();
            lblInfo.Text =
                "Esta funcionalidad solo puede ser usada por el ADMINISTRADOR GENERAL.\n" +
                "Primero inicie sesión como Personal Hospitalario con la cuenta global,\n" +
                "y luego use la opción 'Registrar Nuevo Personal' en el menú de administración.";
            lblInfo.Font = new Font("Segoe UI", 11);
            lblInfo.Location = new Point(100, 100);
            lblInfo.Size = new Size(600, 100);
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfo);

            Label lblCredenciales = new Label();
            lblCredenciales.Text =
                "Credencial de ADMINISTRADOR GENERAL por defecto:\n" +
                "ID: ADMIN001\nPassword: admin123";
            lblCredenciales.Font = new Font("Consolas", 11, FontStyle.Bold);
            lblCredenciales.Location = new Point(250, 230);
            lblCredenciales.Size = new Size(300, 80);
            lblCredenciales.TextAlign = ContentAlignment.MiddleCenter;
            lblCredenciales.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblCredenciales);

            Button btnCerrar = new Button();
            btnCerrar.Text = "Entendido";
            btnCerrar.Font = new Font("Segoe UI", 12);
            btnCerrar.Location = new Point(320, 360);
            btnCerrar.Size = new Size(160, 50);
            btnCerrar.BackColor = Color.White;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }
    }
}