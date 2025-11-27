using System;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormLoginPersonal : Form
    {
        private Sistema sistema;
        private TextBox txtID;
        private TextBox txtPassword;

        public FormLoginPersonal(Sistema sistemaParam)
        {
            sistema = sistemaParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(800, 600);
            this.Text = "Login - Personal Hospitalario";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            TabControl tabControl = new TabControl();
            tabControl.Location = new Point(30, 50);
            tabControl.Size = new Size(740, 500);
            tabControl.Font = new Font("Segoe UI", 10);
            this.Controls.Add(tabControl);

            TabPage tabLogin = new TabPage("Iniciar Sesión");
            tabControl.TabPages.Add(tabLogin);
            CrearTabLogin(tabLogin);

            TabPage tabRegistro = new TabPage("Registro");
            tabControl.TabPages.Add(tabRegistro);
            CrearTabRegistro(tabRegistro);

            this.ResumeLayout(false);
        }

        private void CrearTabLogin(TabPage tab)
        {
            Label lblTitulo = new Label();
            lblTitulo.Text = "¡Bienvenido Doctor!";
            lblTitulo.Font = new Font("Segoe UI", 22, FontStyle.Bold | FontStyle.Italic);
            lblTitulo.Location = new Point(200, 50);
            lblTitulo.Size = new Size(400, 50);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            tab.Controls.Add(lblTitulo);

            Label lblID = new Label();
            lblID.Text = "ID:";
            lblID.Font = new Font("Segoe UI", 12);
            lblID.Location = new Point(150, 150);
            lblID.Size = new Size(100, 30);
            lblID.TextAlign = ContentAlignment.MiddleRight;
            tab.Controls.Add(lblID);

            txtID = new TextBox();
            txtID.Font = new Font("Segoe UI", 12);
            txtID.Location = new Point(270, 150);
            txtID.Size = new Size(250, 30);
            tab.Controls.Add(txtID);

            Label lblPassword = new Label();
            lblPassword.Text = "Contraseña:";
            lblPassword.Font = new Font("Segoe UI", 12);
            lblPassword.Location = new Point(150, 210);
            lblPassword.Size = new Size(110, 30);
            lblPassword.TextAlign = ContentAlignment.MiddleRight;
            tab.Controls.Add(lblPassword);

            txtPassword = new TextBox();
            txtPassword.Font = new Font("Segoe UI", 12);
            txtPassword.Location = new Point(270, 210);
            txtPassword.Size = new Size(250, 30);
            txtPassword.UseSystemPasswordChar = true;
            tab.Controls.Add(txtPassword);

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            btnCancelar.Location = new Point(220, 290);
            btnCancelar.Size = new Size(130, 45);
            btnCancelar.BackColor = Color.White;
            btnCancelar.Click += (s, e) => this.Close();
            tab.Controls.Add(btnCancelar);

            Button btnIniciar = new Button();
            btnIniciar.Text = "Iniciar";
            btnIniciar.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            btnIniciar.Location = new Point(380, 290);
            btnIniciar.Size = new Size(130, 45);
            btnIniciar.BackColor = Color.White;
            btnIniciar.Click += BtnIniciar_Click;
            tab.Controls.Add(btnIniciar);

            Label lblInstruccion = new Label();
            lblInstruccion.Text = "El sistema está listo para trabajar con usted";
            lblInstruccion.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            lblInstruccion.Location = new Point(180, 370);
            lblInstruccion.Size = new Size(400, 25);
            lblInstruccion.TextAlign = ContentAlignment.MiddleCenter;
            tab.Controls.Add(lblInstruccion);
        }

        private void CrearTabRegistro(TabPage tab)
        {
            Label lblInfo = new Label();
            lblInfo.Text = "REGISTRO DE PERSONAL\n(Solo Administrador General)";
            lblInfo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblInfo.Location = new Point(150, 100);
            lblInfo.Size = new Size(450, 80);
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            tab.Controls.Add(lblInfo);

            Label lblDescripcion = new Label();
            lblDescripcion.Text =
                "Para registrar nuevo personal hospitalario,\n" +
                "el ADMINISTRADOR GENERAL debe autenticarse primero\n" +
                "y usar la opción 'Registrar Nuevo Personal' en su panel.";
            lblDescripcion.Font = new Font("Segoe UI", 12);
            lblDescripcion.Location = new Point(150, 200);
            lblDescripcion.Size = new Size(450, 80);
            lblDescripcion.TextAlign = ContentAlignment.MiddleCenter;
            tab.Controls.Add(lblDescripcion);

            Button btnRegistrarPersonal = new Button();
            btnRegistrarPersonal.Text = "Continuar como Administrador";
            btnRegistrarPersonal.Font = new Font("Segoe UI", 12);
            btnRegistrarPersonal.Location = new Point(220, 300);
            btnRegistrarPersonal.Size = new Size(300, 50);
            btnRegistrarPersonal.BackColor = Color.White;
            btnRegistrarPersonal.Click += (s, e) => AbrirRegistroPersonal();
            tab.Controls.Add(btnRegistrarPersonal);
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor complete todos los campos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PersonalHospitalario personal = sistema.BuscarPersonal(id, password);

            if (personal != null)
            {
                MessageBox.Show($"Acceso concedido\nBienvenido, {personal.Nombre}", "Inicio exitoso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!personal.CambioPassword && password == "medicenter2025")
                {
                    FormCambiarPassword formCambio = new FormCambiarPassword(sistema, personal);
                    if (formCambio.ShowDialog() == DialogResult.OK)
                    {
                        AbrirMenuPersonal(personal);
                    }
                }
                else
                {
                    AbrirMenuPersonal(personal);
                }
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas", "Error de autenticación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirMenuPersonal(PersonalHospitalario personal)
        {
            this.Hide();

            if (personal.NivelAcceso == NivelAcceso.Administrador)
            {
                FormMenuAdministrador formMenu = new FormMenuAdministrador(sistema, personal);
                formMenu.ShowDialog();
            }
            else
            {
                FormMenuMedico formMenu = new FormMenuMedico(sistema, personal);
                formMenu.ShowDialog();
            }

            this.Close();
        }

        private void AbrirRegistroPersonal()
        {
            FormRegistroPersonal formRegistro = new FormRegistroPersonal(sistema);
            formRegistro.ShowDialog();
        }
    }
}