using System;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormRegistrarPersonalAdmin : Form
    {
        private readonly Sistema sistema;
        private readonly PersonalHospitalario admin;

        private TextBox txtNombre;
        private TextBox txtEmail;
        private ComboBox cmbHospital;
        private ComboBox cmbNivelAcceso;
        private TextBox txtEspecialidad;

        public FormRegistrarPersonalAdmin(Sistema sistemaParam, PersonalHospitalario adminParam)
        {
            sistema = sistemaParam;
            admin = adminParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(700, 520);
            this.Text = "Registrar Nuevo Personal Hospitalario";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblTitulo = new Label();
            lblTitulo.Text = "Registrar Nuevo Personal Hospitalario";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.Location = new Point(120, 20);
            lblTitulo.Size = new Size(460, 40);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            int yPos = 90;

            // Nombre
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre completo:";
            lblNombre.Font = new Font("Segoe UI", 11);
            lblNombre.Location = new Point(60, yPos);
            lblNombre.Size = new Size(150, 30);
            lblNombre.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblNombre);

            txtNombre = new TextBox();
            txtNombre.Font = new Font("Segoe UI", 11);
            txtNombre.Location = new Point(220, yPos);
            txtNombre.Size = new Size(380, 30);
            this.Controls.Add(txtNombre);

            yPos += 45;

            // Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Font = new Font("Segoe UI", 11);
            lblEmail.Location = new Point(60, yPos);
            lblEmail.Size = new Size(150, 30);
            lblEmail.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Font = new Font("Segoe UI", 11);
            txtEmail.Location = new Point(220, yPos);
            txtEmail.Size = new Size(380, 30);
            this.Controls.Add(txtEmail);

            yPos += 45;

            // Hospital
            Label lblHospital = new Label();
            lblHospital.Text = "Hospital:";
            lblHospital.Font = new Font("Segoe UI", 11);
            lblHospital.Location = new Point(60, yPos);
            lblHospital.Size = new Size(150, 30);
            lblHospital.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblHospital);

            cmbHospital = new ComboBox();
            cmbHospital.Font = new Font("Segoe UI", 11);
            cmbHospital.Location = new Point(220, yPos);
            cmbHospital.Size = new Size(380, 30);
            cmbHospital.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (var hosp in sistema.Hospitales)
            {
                // Ej: "H001 - Hospital Nacional de Nicaragua"
                cmbHospital.Items.Add($"{hosp.Id} - {hosp.Nombre}");
            }

            if (cmbHospital.Items.Count > 0)
                cmbHospital.SelectedIndex = 0;

            this.Controls.Add(cmbHospital);

            yPos += 45;

            // Nivel de acceso
            Label lblNivel = new Label();
            lblNivel.Text = "Nivel de acceso:";
            lblNivel.Font = new Font("Segoe UI", 11);
            lblNivel.Location = new Point(60, yPos);
            lblNivel.Size = new Size(150, 30);
            lblNivel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblNivel);

            cmbNivelAcceso = new ComboBox();
            cmbNivelAcceso.Font = new Font("Segoe UI", 11);
            cmbNivelAcceso.Location = new Point(220, yPos);
            cmbNivelAcceso.Size = new Size(250, 30);
            cmbNivelAcceso.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNivelAcceso.Items.Add("Médico General");
            cmbNivelAcceso.Items.Add("Administrador");
            cmbNivelAcceso.SelectedIndex = 0;
            this.Controls.Add(cmbNivelAcceso);

            yPos += 45;

            // Especialidad (solo se usa si es médico)
            Label lblEsp = new Label();
            lblEsp.Text = "Especialidad (si es médico):";
            lblEsp.Font = new Font("Segoe UI", 11);
            lblEsp.Location = new Point(10, yPos);
            lblEsp.Size = new Size(200, 30);
            lblEsp.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblEsp);

            txtEspecialidad = new TextBox();
            txtEspecialidad.Font = new Font("Segoe UI", 11);
            txtEspecialidad.Location = new Point(220, yPos);
            txtEspecialidad.Size = new Size(380, 30);
            this.Controls.Add(txtEspecialidad);

            yPos += 60;

            // Info sobre password por defecto
            Label lblInfoPass = new Label();
            lblInfoPass.Text = "La contraseña por defecto será: medicenter2025\n" +
                               "El usuario deberá cambiarla al iniciar sesión.";
            lblInfoPass.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            lblInfoPass.Location = new Point(120, yPos);
            lblInfoPass.Size = new Size(460, 40);
            lblInfoPass.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfoPass);

            yPos += 70;

            // Botones
            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 11);
            btnCancelar.Location = new Point(190, yPos);
            btnCancelar.Size = new Size(140, 45);
            btnCancelar.BackColor = Color.White;
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);

            Button btnRegistrar = new Button();
            btnRegistrar.Text = "Registrar";
            btnRegistrar.Font = new Font("Segoe UI", 11);
            btnRegistrar.Location = new Point(360, yPos);
            btnRegistrar.Size = new Size(140, 45);
            btnRegistrar.BackColor = Color.White;
            btnRegistrar.Click += BtnRegistrar_Click;
            this.Controls.Add(btnRegistrar);
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre completo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Ingrese un email válido", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbHospital.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un hospital", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbNivelAcceso.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione el nivel de acceso", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener ID de hospital (ej: "H001" de "H001 - Nombre")
            string seleccionado = cmbHospital.SelectedItem.ToString();
            string idHospital = seleccionado.Split('-')[0].Trim();

            Hospital hospital = sistema.BuscarHospital(idHospital);
            if (hospital == null)
            {
                MessageBox.Show("No se encontró el hospital seleccionado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Nivel de acceso
            NivelAcceso nivel;
            if (cmbNivelAcceso.SelectedItem.ToString().StartsWith("Médico"))
                nivel = NivelAcceso.MedicoGeneral;
            else
                nivel = NivelAcceso.Administrador;

            // Generar ID y contraseña por defecto
            string nuevoId = sistema.GenerarIdPersonal();
            string passwordDefecto = "medicenter2025";

            PersonalHospitalario nuevo = new PersonalHospitalario
            {
                Id = nuevoId,
                Nombre = txtNombre.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Password = passwordDefecto,
                IdHospital = idHospital,
                NivelAcceso = nivel
            };

            if (nivel == NivelAcceso.MedicoGeneral)
            {
                nuevo.Especialidad = txtEspecialidad.Text.Trim();
            }

            sistema.Personal.Add(nuevo);
            hospital.PersonalIds.Add(nuevo.Id);
            sistema.GuardarTodosDatos();

            string mensaje = "Personal registrado exitosamente\n\n";
            mensaje += $"ID: {nuevo.Id}\n";
            mensaje += $"Nivel: {nuevo.NivelAcceso}\n";
            mensaje += $"Hospital: {hospital.Nombre}\n\n";
            mensaje += $"Contraseña por defecto: {passwordDefecto}\n";
            mensaje += "El usuario deberá cambiarla al iniciar sesión.";

            MessageBox.Show(mensaje, "Registro exitoso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}