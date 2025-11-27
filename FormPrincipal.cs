using System;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormPrincipal : Form
    {
        private Sistema sistema;

        public FormPrincipal()
        {
            sistema = new Sistema();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(900, 600);
            this.Text = "MediCenter - Sistema de Gestión Médica Integral";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Icon = SystemIcons.Application;

            Label lblTitulo = new Label();
            lblTitulo.Text = "¡Bienvenido a MediCenter!";
            lblTitulo.Font = new Font("Segoe UI", 24, FontStyle.Bold | FontStyle.Italic);
            lblTitulo.Location = new Point(200, 50);
            lblTitulo.Size = new Size(500, 50);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            Label lblSubtitulo = new Label();
            lblSubtitulo.Text = "Sistema de Gestión Médica Integral";
            lblSubtitulo.Font = new Font("Segoe UI", 16, FontStyle.Italic);
            lblSubtitulo.Location = new Point(200, 100);
            lblSubtitulo.Size = new Size(500, 40);
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblSubtitulo);

            Panel panelPaciente = new Panel();
            panelPaciente.Location = new Point(100, 180);
            panelPaciente.Size = new Size(300, 300);
            panelPaciente.BorderStyle = BorderStyle.FixedSingle;
            panelPaciente.BackColor = Color.White;
            panelPaciente.Cursor = Cursors.Hand;
            this.Controls.Add(panelPaciente);

            // PictureBox para imagen de Paciente
            PictureBox picPaciente = new PictureBox();
            picPaciente.Location = new Point(75, 30);
            picPaciente.Size = new Size(150, 150);
            picPaciente.BorderStyle = BorderStyle.FixedSingle;
            picPaciente.SizeMode = PictureBoxSizeMode.Zoom;
            panelPaciente.Controls.Add(picPaciente);

            Label lblPaciente = new Label();
            lblPaciente.Text = "Paciente";
            lblPaciente.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblPaciente.Location = new Point(0, 220);
            lblPaciente.Size = new Size(300, 40);
            lblPaciente.TextAlign = ContentAlignment.MiddleCenter;
            panelPaciente.Controls.Add(lblPaciente);

            panelPaciente.Click += (s, e) => AbrirLoginPaciente();
            picPaciente.Click += (s, e) => AbrirLoginPaciente();
            lblPaciente.Click += (s, e) => AbrirLoginPaciente();

            Panel panelPersonal = new Panel();
            panelPersonal.Location = new Point(500, 180);
            panelPersonal.Size = new Size(300, 300);
            panelPersonal.BorderStyle = BorderStyle.FixedSingle;
            panelPersonal.BackColor = Color.White;
            panelPersonal.Cursor = Cursors.Hand;
            this.Controls.Add(panelPersonal);

            // PictureBox para imagen de Personal Hospitalario
            PictureBox picPersonal = new PictureBox();
            picPersonal.Location = new Point(75, 30);
            picPersonal.Size = new Size(150, 150);
            picPersonal.BorderStyle = BorderStyle.FixedSingle;
            picPersonal.SizeMode = PictureBoxSizeMode.Zoom;
            panelPersonal.Controls.Add(picPersonal);

            Label lblPersonal = new Label();
            lblPersonal.Text = "Personal Hospitalario";
            lblPersonal.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblPersonal.Location = new Point(0, 220);
            lblPersonal.Size = new Size(300, 40);
            lblPersonal.TextAlign = ContentAlignment.MiddleCenter;
            panelPersonal.Controls.Add(lblPersonal);

            panelPersonal.Click += (s, e) => AbrirLoginPersonal();
            picPersonal.Click += (s, e) => AbrirLoginPersonal();
            lblPersonal.Click += (s, e) => AbrirLoginPersonal();

            Label lblInstruccion = new Label();
            lblInstruccion.Text = "Seleccione una opción para continuar";
            lblInstruccion.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            lblInstruccion.Location = new Point(200, 500);
            lblInstruccion.Size = new Size(500, 30);
            lblInstruccion.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInstruccion);

            this.ResumeLayout(false);
        }

        private void AbrirLoginPaciente()
        {
            FormLoginPaciente formLogin = new FormLoginPaciente(sistema);
            formLogin.ShowDialog();
        }

        private void AbrirLoginPersonal()
        {
            FormLoginPersonal formLogin = new FormLoginPersonal(sistema);
            formLogin.ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            sistema.GuardarTodosDatos();
        }
    }
}