using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormMenuAdministrador : Form
    {
        private Sistema sistema;
        private PersonalHospitalario admin;

        public FormMenuAdministrador(Sistema sistemaParam, PersonalHospitalario adminParam)
        {
            sistema = sistemaParam;
            admin = adminParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 700);
            this.Text = "MediCenter - Menú Administrador";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Hospital hospital = sistema.BuscarHospital(admin.IdHospital);

            Panel panelInfo = new Panel();
            panelInfo.Location = new Point(0, 0);
            panelInfo.Size = new Size(900, 100);
            panelInfo.BackColor = Color.FromArgb(200, 100, 100);
            this.Controls.Add(panelInfo);

            Label lblNombreAdmin = new Label();
            if (admin.Id == "ADMIN001")
            {
                lblNombreAdmin.Text = $"ADMINISTRADOR GENERAL - {admin.Nombre}";
            }
            else
            {
                lblNombreAdmin.Text = $"ADMINISTRADOR DEL HOSPITAL - {admin.Nombre}";
            }
            lblNombreAdmin.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblNombreAdmin.ForeColor = Color.White;
            lblNombreAdmin.Location = new Point(30, 20);
            lblNombreAdmin.Size = new Size(600, 35);
            panelInfo.Controls.Add(lblNombreAdmin);

            Label lblHospital = new Label();
            lblHospital.Font = new Font("Segoe UI", 13);
            lblHospital.ForeColor = Color.White;
            lblHospital.Location = new Point(30, 55);
            lblHospital.Size = new Size(600, 30);

            // En el administrador general no se muestra el nombre de hospital,
            // en los administradores de hospital sí.
            if (admin.Id == "ADMIN001")
            {
                lblHospital.Text = string.Empty;
            }
            else
            {
                lblHospital.Text = hospital?.Nombre ?? "Hospital";
            }

            panelInfo.Controls.Add(lblHospital);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Panel de Administración";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(300, 120);
            lblTitulo.Size = new Size(300, 40);
            this.Controls.Add(lblTitulo);

            int yPos = 180;
            int xLeft = 100;
            int xRight = 500;

            // 1. Ver información de los hospitales
            Button btnEstadisticas = CrearBoton("1. Ver Información de los Hospitales", xLeft, yPos);
            btnEstadisticas.Click += (s, e) => VerEstadisticas();
            this.Controls.Add(btnEstadisticas);

            // 2. Cambiar contraseña (mi usuario)
            Button btnPassword = CrearBoton("2. Cambiar Contraseña", xRight, yPos);
            btnPassword.Click += (s, e) => CambiarPassword();
            this.Controls.Add(btnPassword);

            yPos += 70;

            // 3. Registrar nuevo personal medico
            Button btnRegistrar = CrearBoton("3. Registrar Nuevo Personal Médico", xLeft, yPos);
            btnRegistrar.Click += (s, e) => RegistrarNuevoPersonal();
            this.Controls.Add(btnRegistrar);

            // 4. Ver mi información
            Button btnInfo = CrearBoton("4. Ver mi Información", xRight, yPos);
            btnInfo.Click += (s, e) => VerInformacion();
            this.Controls.Add(btnInfo);

            yPos += 70;

            // 5. Gestionar/Ver personal del hospital
            Button btnPersonal = CrearBoton("5. Gestionar/Ver Personal del Hospital", xLeft, yPos);
            btnPersonal.Click += (s, e) => GestionarPersonal();
            this.Controls.Add(btnPersonal);

            // 6. Ver información de pacientes y doctores
            Button btnPacientesDoctores = CrearBoton("6. Ver Información de Pacientes y Doctores", xRight, yPos);
            btnPacientesDoctores.Click += (s, e) => VerInformacionPacientesYDoctores();
            this.Controls.Add(btnPacientesDoctores);

            yPos += 70;

            // 7. Trasladar paciente (desde administrador de hospitales)
            Button btnTraslado = CrearBoton("7. Trasladar Paciente", xLeft, yPos);
            btnTraslado.Click += (s, e) => TrasladarPacienteDesdeAdmin();
            this.Controls.Add(btnTraslado);

            yPos += 70;

            // 0. Cerrar sesión
            Button btnCerrarSesion = CrearBoton("0. Cerrar Sesión", xLeft, yPos);
            btnCerrarSesion.BackColor = Color.FromArgb(200, 100, 100);
            btnCerrarSesion.Click += (s, e) => CerrarSesion();
            this.Controls.Add(btnCerrarSesion);
        }
        private void RegistrarNuevoPersonal()
        {
            FormRegistrarPersonalAdmin form = new FormRegistrarPersonalAdmin(sistema, admin);
            form.ShowDialog();
        }

        private Button CrearBoton(string texto, int x, int y)
        {
            Button btn = new Button();
            btn.Text = texto;
            btn.Font = new Font("Segoe UI", 11);
            btn.Location = new Point(x, y);
            btn.Size = new Size(350, 55);
            btn.BackColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private void GestionarPersonal()
        {
            FormGestionPersonal formGestion = new FormGestionPersonal(sistema, admin);
            formGestion.ShowDialog();
        }

        private void VerEstadisticas()
        {
            // Ahora se usa el formulario de comparación para mostrar información de hospitales.
            FormCompararHospitales form = new FormCompararHospitales(sistema);
            form.ShowDialog();
        }

        private void VerRegistrosMedicos()
        {
            FormRegistrosMedicosHospital formRegistros =
                new FormRegistrosMedicosHospital(sistema, admin.IdHospital);
            formRegistros.ShowDialog();
        }

        private void VerColaPacientes()
        {
            FormColaPacientes formCola = new FormColaPacientes(sistema, admin.IdHospital);
            formCola.ShowDialog();
        }

        private void VerInformacion()
        {
            string info = "═══════════════════════════════════════\n" +
                         "       INFORMACIÓN DEL ADMINISTRADOR\n" +
                         "═══════════════════════════════════════\n\n" +
                         $"ID: {admin.Id}\n" +
                         $"Nombre: {admin.Nombre}\n" +
                         $"Email: {admin.Email}\n" +
                         $"Hospital: {admin.IdHospital}\n" +
                         $"Nivel de Acceso: {admin.NivelAcceso}\n";

            MessageBox.Show(info, "Mi información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CambiarPassword()
        {
            FormCambiarPasswordPersonal formCambio = new FormCambiarPasswordPersonal(sistema, admin);
            formCambio.ShowDialog();
        }

        private void CerrarSesion()
        {
            sistema.GuardarTodosDatos();
            MessageBox.Show("Sesión guardada. Hasta pronto", "Cerrando sesión",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void TrasladarPacienteDesdeAdmin()
        {
            using (FormTrasladoPaciente form = new FormTrasladoPaciente(sistema))
            {
                form.ShowDialog();
            }
        }

        // Nueva opción: ver información global de pacientes y doctores
        private void VerInformacionPacientesYDoctores()
        {
            using (FormInfoPacientesDoctores form = new FormInfoPacientesDoctores(sistema))
            {
                form.ShowDialog();
            }
        }

    }
}
