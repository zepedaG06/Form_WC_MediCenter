using System;
using System.Drawing;
using System.Windows.Forms;


namespace MEDICENTER
{
    public partial class FormLoginPaciente : Form
    {
        private Sistema sistema;
        private TextBox txtID;
        private TextBox txtPassword;
        private CheckBox chkVerPassword;

        public FormLoginPaciente(Sistema sistemaParam)
        {
            sistema = sistemaParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(900, 700);
            this.Text = "Login - Paciente";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Tabs
            TabControl tabControl = new TabControl();
            tabControl.Location = new Point(30, 50);
            tabControl.Size = new Size(840, 600);
            tabControl.Font = new Font("Segoe UI", 10);
            this.Controls.Add(tabControl);

            // Tab Iniciar Sesión
            TabPage tabLogin = new TabPage("Iniciar Sesión");
            tabControl.TabPages.Add(tabLogin);
            CrearTabLogin(tabLogin);

            // Tab Registrarse
            TabPage tabRegistro = new TabPage("Registrarse");
            tabControl.TabPages.Add(tabRegistro);
            CrearTabRegistro(tabRegistro);

            this.ResumeLayout(false);
        }

        private void CrearTabLogin(TabPage tab)
        {
            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "Bienvenido Paciente";
            lblTitulo.Font = new Font("Segoe UI", 22, FontStyle.Bold | FontStyle.Italic);
            lblTitulo.Location = new Point(250, 50);
            lblTitulo.Size = new Size(400, 50);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            tab.Controls.Add(lblTitulo);

            // Logo (panel simulado)
            Panel panelLogo = new Panel();
            panelLogo.Location = new Point(650, 120);
            panelLogo.Size = new Size(150, 150);
            panelLogo.BackColor = Color.FromArgb(100, 180, 220);
            tab.Controls.Add(panelLogo);

            Label lblLogo = new Label();
            lblLogo.Text = "MEDICENTER\nRAPID DIAGNOSTIC SYSTEM";
            lblLogo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblLogo.Location = new Point(640, 280);
            lblLogo.Size = new Size(170, 50);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            tab.Controls.Add(lblLogo);

            // ID Usuario
            Label lblID = new Label();
            lblID.Text = "ID Usuario:";
            lblID.Font = new Font("Segoe UI", 12);
            lblID.Location = new Point(150, 180);
            lblID.Size = new Size(120, 30);
            lblID.TextAlign = ContentAlignment.MiddleRight;
            tab.Controls.Add(lblID);

            txtID = new TextBox();
            txtID.Font = new Font("Segoe UI", 12);
            txtID.Location = new Point(280, 180);
            txtID.Size = new Size(250, 30);
            tab.Controls.Add(txtID);

            // Contraseña
            Label lblPassword = new Label();
            lblPassword.Text = "Contraseña:";
            lblPassword.Font = new Font("Segoe UI", 12);
            lblPassword.Location = new Point(150, 240);
            lblPassword.Size = new Size(120, 30);
            lblPassword.TextAlign = ContentAlignment.MiddleRight;
            tab.Controls.Add(lblPassword);

            txtPassword = new TextBox();
            txtPassword.Font = new Font("Segoe UI", 12);
            txtPassword.Location = new Point(280, 240);
            txtPassword.Size = new Size(250, 30);
            txtPassword.UseSystemPasswordChar = true;
            tab.Controls.Add(txtPassword);

            // Ver contraseña
            chkVerPassword = new CheckBox();
            chkVerPassword.Text = "Ver Contraseña";
            chkVerPassword.Font = new Font("Segoe UI", 10);
            chkVerPassword.Location = new Point(550, 240);
            chkVerPassword.Size = new Size(150, 30);
            chkVerPassword.CheckedChanged += ChkVerPassword_CheckedChanged;
            tab.Controls.Add(chkVerPassword);

            // Olvidaste tu contraseña
            LinkLabel linkOlvido = new LinkLabel();
            linkOlvido.Text = "¿Olvidaste tu contraseña?";
            linkOlvido.Font = new Font("Segoe UI", 10, FontStyle.Underline);
            linkOlvido.Location = new Point(280, 300);
            linkOlvido.Size = new Size(200, 25);
            linkOlvido.LinkColor = Color.Blue;
            tab.Controls.Add(linkOlvido);

            // Botones
            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            btnCancelar.Location = new Point(230, 370);
            btnCancelar.Size = new Size(130, 45);
            btnCancelar.BackColor = Color.White;
            btnCancelar.Click += (s, e) => this.Close();
            tab.Controls.Add(btnCancelar);

            Button btnIngresar = new Button();
            btnIngresar.Text = "Ingresar";
            btnIngresar.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            btnIngresar.Location = new Point(400, 370);
            btnIngresar.Size = new Size(130, 45);
            btnIngresar.BackColor = Color.White;
            btnIngresar.Click += BtnIngresar_Click;
            tab.Controls.Add(btnIngresar);

            // Instrucción
            Label lblInstruccion = new Label();
            lblInstruccion.Text = "Ingresa tus credenciales para acceder al sistema";
            lblInstruccion.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            lblInstruccion.Location = new Point(200, 460);
            lblInstruccion.Size = new Size(400, 25);
            lblInstruccion.TextAlign = ContentAlignment.MiddleCenter;
            tab.Controls.Add(lblInstruccion);
        }

        private void CrearTabRegistro(TabPage tab)
        {
            // Scroll panel para el formulario
            Panel scrollPanel = new Panel();
            scrollPanel.Location = new Point(10, 10);
            scrollPanel.Size = new Size(810, 540);
            scrollPanel.AutoScroll = true;
            tab.Controls.Add(scrollPanel);

            int yPos = 20;

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "Crea tu cuenta";
            lblTitulo.Font = new Font("Segoe UI", 22, FontStyle.Bold | FontStyle.Italic);
            lblTitulo.Location = new Point(30, yPos);
            lblTitulo.Size = new Size(400, 50);
            scrollPanel.Controls.Add(lblTitulo);

            // Logo
            Panel panelLogo = new Panel();
            panelLogo.Location = new Point(620, yPos);
            panelLogo.Size = new Size(150, 150);
            panelLogo.BackColor = Color.FromArgb(100, 180, 220);
            scrollPanel.Controls.Add(panelLogo);

            Label lblLogo = new Label();
            lblLogo.Text = "MEDICENTER\nRAPID DIAGNOSTIC SYSTEM";
            lblLogo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblLogo.Location = new Point(610, yPos + 160);
            lblLogo.Size = new Size(170, 50);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            scrollPanel.Controls.Add(lblLogo);

            yPos += 80;

            // ID (auto-generado, solo lectura)
            Label lblID = new Label();
            lblID.Text = "ID:";
            lblID.Font = new Font("Segoe UI", 11);
            lblID.Location = new Point(30, yPos);
            lblID.Size = new Size(150, 30);
            lblID.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblID);

            TextBox txtIDReg = new TextBox();
            txtIDReg.Font = new Font("Segoe UI", 11);
            txtIDReg.Location = new Point(190, yPos);
            txtIDReg.Size = new Size(300, 30);
            txtIDReg.ReadOnly = true;
            txtIDReg.BackColor = Color.LightGray;
            txtIDReg.Text = "(Auto-generado)";
            scrollPanel.Controls.Add(txtIDReg);

            yPos += 45;

            // Nombre Completo
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre Completo:";
            lblNombre.Font = new Font("Segoe UI", 11);
            lblNombre.Location = new Point(30, yPos);
            lblNombre.Size = new Size(150, 30);
            lblNombre.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox();
            txtNombre.Font = new Font("Segoe UI", 11);
            txtNombre.Location = new Point(190, yPos);
            txtNombre.Size = new Size(300, 30);
            txtNombre.Tag = "nombre";
            scrollPanel.Controls.Add(txtNombre);

            yPos += 45;

            // Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Font = new Font("Segoe UI", 11);
            lblEmail.Location = new Point(30, yPos);
            lblEmail.Size = new Size(150, 30);
            lblEmail.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblEmail);

            TextBox txtEmail = new TextBox();
            txtEmail.Font = new Font("Segoe UI", 11);
            txtEmail.Location = new Point(190, yPos);
            txtEmail.Size = new Size(300, 30);
            txtEmail.Tag = "email";
            scrollPanel.Controls.Add(txtEmail);

            yPos += 45;

            // Contraseña
            Label lblPassReg = new Label();
            lblPassReg.Text = "Contraseña:";
            lblPassReg.Font = new Font("Segoe UI", 11);
            lblPassReg.Location = new Point(30, yPos);
            lblPassReg.Size = new Size(150, 30);
            lblPassReg.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblPassReg);

            TextBox txtPassReg = new TextBox();
            txtPassReg.Font = new Font("Segoe UI", 11);
            txtPassReg.Location = new Point(190, yPos);
            txtPassReg.Size = new Size(300, 30);
            txtPassReg.UseSystemPasswordChar = true;
            txtPassReg.Tag = "password";
            scrollPanel.Controls.Add(txtPassReg);

            yPos += 45;

            // Edad
            Label lblEdad = new Label();
            lblEdad.Text = "Edad:";
            lblEdad.Font = new Font("Segoe UI", 11);
            lblEdad.Location = new Point(30, yPos);
            lblEdad.Size = new Size(150, 30);
            lblEdad.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblEdad);

            NumericUpDown numEdad = new NumericUpDown();
            numEdad.Font = new Font("Segoe UI", 11);
            numEdad.Location = new Point(190, yPos);
            numEdad.Size = new Size(100, 30);
            numEdad.Minimum = 0;
            numEdad.Maximum = 120;
            numEdad.Tag = "edad";
            scrollPanel.Controls.Add(numEdad);

            CheckBox chkVerPassReg = new CheckBox();
            chkVerPassReg.Text = "Ver Contraseña";
            chkVerPassReg.Font = new Font("Segoe UI", 10);
            chkVerPassReg.Location = new Point(320, yPos);
            chkVerPassReg.Size = new Size(150, 30);
            chkVerPassReg.CheckedChanged += (s, e) =>
            {
                txtPassReg.UseSystemPasswordChar = !chkVerPassReg.Checked;
            };
            scrollPanel.Controls.Add(chkVerPassReg);

            yPos += 45;

            // Género
            Label lblGenero = new Label();
            lblGenero.Text = "Género:";
            lblGenero.Font = new Font("Segoe UI", 11);
            lblGenero.Location = new Point(30, yPos);
            lblGenero.Size = new Size(150, 30);
            lblGenero.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblGenero);

            ComboBox cmbGenero = new ComboBox();
            cmbGenero.Font = new Font("Segoe UI", 11);
            cmbGenero.Location = new Point(190, yPos);
            cmbGenero.Size = new Size(200, 30);
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGenero.Items.AddRange(new object[] { "Masculino", "Femenino" });
            cmbGenero.Tag = "genero";
            scrollPanel.Controls.Add(cmbGenero);

            yPos += 45;

            // Teléfono
            Label lblTelefono = new Label();
            lblTelefono.Text = "Teléfono:";
            lblTelefono.Font = new Font("Segoe UI", 11);
            lblTelefono.Location = new Point(30, yPos);
            lblTelefono.Size = new Size(150, 30);
            lblTelefono.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblTelefono);

            TextBox txtTelefono = new TextBox();
            txtTelefono.Font = new Font("Segoe UI", 11);
            txtTelefono.Location = new Point(190, yPos);
            txtTelefono.Size = new Size(200, 30);
            txtTelefono.Tag = "telefono";
            scrollPanel.Controls.Add(txtTelefono);

            // Contacto Emergencia
            Label lblContactoEmerg = new Label();
            lblContactoEmerg.Text = "Contacto Emergencia:";
            lblContactoEmerg.Font = new Font("Segoe UI", 11);
            lblContactoEmerg.Location = new Point(400, yPos);
            lblContactoEmerg.Size = new Size(170, 30);
            lblContactoEmerg.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblContactoEmerg);

            TextBox txtContactoEmerg = new TextBox();
            txtContactoEmerg.Font = new Font("Segoe UI", 11);
            txtContactoEmerg.Location = new Point(580, yPos);
            txtContactoEmerg.Size = new Size(200, 30);
            txtContactoEmerg.Tag = "contacto";
            scrollPanel.Controls.Add(txtContactoEmerg);

            yPos += 45;

            // Tipo de Sangre
            Label lblTipoSangre = new Label();
            lblTipoSangre.Text = "Tipo De Sangre:";
            lblTipoSangre.Font = new Font("Segoe UI", 11);
            lblTipoSangre.Location = new Point(30, yPos);
            lblTipoSangre.Size = new Size(150, 30);
            lblTipoSangre.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblTipoSangre);

            ComboBox cmbTipoSangre = new ComboBox();
            cmbTipoSangre.Font = new Font("Segoe UI", 11);
            cmbTipoSangre.Location = new Point(190, yPos);
            cmbTipoSangre.Size = new Size(150, 30);
            cmbTipoSangre.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoSangre.Items.AddRange(new object[] {
                "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-"
            });
            cmbTipoSangre.Tag = "tipoSangre";
            scrollPanel.Controls.Add(cmbTipoSangre);

            // Cédula
            Label lblCedula = new Label();
            lblCedula.Text = "Cédula:";
            lblCedula.Font = new Font("Segoe UI", 11);
            lblCedula.Location = new Point(400, yPos);
            lblCedula.Size = new Size(170, 30);
            lblCedula.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblCedula);

            TextBox txtCedula = new TextBox();
            txtCedula.Font = new Font("Segoe UI", 11);
            txtCedula.Location = new Point(580, yPos);
            txtCedula.Size = new Size(200, 30);
            scrollPanel.Controls.Add(txtCedula);

            yPos += 45;

            // Seguro Médico
            Label lblSeguro = new Label();
            lblSeguro.Text = "Seguro Médico:";
            lblSeguro.Font = new Font("Segoe UI", 11);
            lblSeguro.Location = new Point(30, yPos);
            lblSeguro.Size = new Size(150, 30);
            lblSeguro.TextAlign = ContentAlignment.MiddleRight;
            scrollPanel.Controls.Add(lblSeguro);

            RadioButton rbSi = new RadioButton();
            rbSi.Text = "Sí";
            rbSi.Font = new Font("Segoe UI", 11);
            rbSi.Location = new Point(190, yPos);
            rbSi.Size = new Size(60, 30);
            rbSi.Tag = "seguro";
            scrollPanel.Controls.Add(rbSi);

            RadioButton rbNo = new RadioButton();
            rbNo.Text = "No";
            rbNo.Font = new Font("Segoe UI", 11);
            rbNo.Location = new Point(270, yPos);
            rbNo.Size = new Size(60, 30);
            rbNo.Checked = true;
            scrollPanel.Controls.Add(rbNo);

            yPos += 50;

            // Panel para imagen del seguro
            Panel panelImagenSeguro = new Panel();
            panelImagenSeguro.Location = new Point(190, yPos);
            panelImagenSeguro.Size = new Size(150, 100);
            panelImagenSeguro.BorderStyle = BorderStyle.FixedSingle;
            panelImagenSeguro.BackColor = Color.White;
            scrollPanel.Controls.Add(panelImagenSeguro);

            Button btnImagenAqui = new Button();
            btnImagenAqui.Text = "Imagen aquí";
            btnImagenAqui.Font = new Font("Segoe UI", 10);
            btnImagenAqui.Location = new Point(200, yPos + 110);
            btnImagenAqui.Size = new Size(120, 30);
            scrollPanel.Controls.Add(btnImagenAqui);

            yPos += 160;

            // Botones finales
            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            btnCancelar.Location = new Point(250, yPos);
            btnCancelar.Size = new Size(120, 40);
            btnCancelar.BackColor = Color.White;
            btnCancelar.Click += (s, e) => this.Close();
            scrollPanel.Controls.Add(btnCancelar);

            Button btnAceptar = new Button();
            btnAceptar.Text = "Aceptar";
            btnAceptar.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            btnAceptar.Location = new Point(400, yPos);
            btnAceptar.Size = new Size(120, 40);
            btnAceptar.BackColor = Color.White;
            btnAceptar.Click += (s, e) => RegistrarPaciente(scrollPanel.Controls);
            scrollPanel.Controls.Add(btnAceptar);
        }

        private void ChkVerPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkVerPassword.Checked;
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor complete todos los campos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Paciente paciente = sistema.BuscarPaciente(id, password);

            if (paciente != null)
            {
                MessageBox.Show($"Bienvenido, {paciente.Nombre}", "Acceso concedido",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                FormMenuPaciente formMenu = new FormMenuPaciente(sistema, paciente);
                this.Hide();
                formMenu.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas", "Error de autenticación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegistrarPaciente(Control.ControlCollection controles)
        {
            try
            {
                Paciente nuevoPaciente = new Paciente();
                nuevoPaciente.Id = sistema.GenerarIdPaciente();

                foreach (Control ctrl in controles)
                {
                    if (ctrl is TextBox txt && txt.Tag != null)
                    {
                        string tag = txt.Tag.ToString();
                        if (tag == "nombre") nuevoPaciente.Nombre = txt.Text.Trim();
                        else if (tag == "email") nuevoPaciente.Email = txt.Text.Trim();
                        else if (tag == "password") nuevoPaciente.Password = txt.Text;
                        else if (tag == "telefono") nuevoPaciente.Telefono = txt.Text.Trim();
                        else if (tag == "contacto") nuevoPaciente.ContactoEmergencia = txt.Text.Trim();
                    }
                    else if (ctrl is NumericUpDown num && num.Tag != null && num.Tag.ToString() == "edad")
                    {
                        nuevoPaciente.Edad = (int)num.Value;
                    }
                    else if (ctrl is ComboBox cmb && cmb.Tag != null)
                    {
                        string tag = cmb.Tag.ToString();
                        if (tag == "genero" && cmb.SelectedIndex >= 0)
                        {
                            nuevoPaciente.Genero = (Genero)cmb.SelectedIndex;
                        }
                        else if (tag == "tipoSangre" && cmb.SelectedIndex >= 0)
                        {
                            nuevoPaciente.TipoSangre = (TipoSangre)cmb.SelectedIndex;
                        }
                    }
                    else if (ctrl is RadioButton rb && rb.Checked && rb.Tag != null && rb.Tag.ToString() == "seguro")
                    {
                        if (rb.Text == "Sí")
                            nuevoPaciente.TipoSeguro = TipoSeguro.SeguroCompleto;
                        else
                            nuevoPaciente.TipoSeguro = TipoSeguro.SinSeguro;
                    }
                }

                if (string.IsNullOrWhiteSpace(nuevoPaciente.Nombre) ||
                    string.IsNullOrWhiteSpace(nuevoPaciente.Email) ||
                    string.IsNullOrWhiteSpace(nuevoPaciente.Password))
                {
                    MessageBox.Show("Por favor complete todos los campos obligatorios", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                sistema.Pacientes.Add(nuevoPaciente);
                sistema.GuardarTodosDatos();

                MessageBox.Show($"Paciente registrado exitosamente!\n\nSu ID es: {nuevoPaciente.Id}\n\nGuarde su ID y contraseña para iniciar sesión.",
                    "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar paciente: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}