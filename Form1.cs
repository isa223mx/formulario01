using System.Text.RegularExpressions;

namespace formulario01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //agregar controladores de eventos texchanged a los campos 
            tbEdad.TextChanged += validarEdad;
            tbEstatura.TextChanged += validarEstatura;
            tbTelefono.TextChanged += validarTelefono;
            tbNombre.TextChanged += validarNombre;
            tbApellidos.TextChanged += validarApellidos;

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {

            //obtener datos de los textBox
            string nombres = tbNombre.Text;
            string apellidos = tbApellidos.Text;
            string edad = tbEdad.Text;
            string estatura = tbEstatura.Text;
            string telefono = tbTelefono.Text;

            //obtener el genero seleccionado
            string genero = "";
            if (radioButton1.Checked)
            {
                genero = "Hombre";
            }
            else if (radioButton2.Checked)
            {
                genero = "Mujer";
            }
            //validar que los campos tengan el formato correcto
            if (EsEnteroValido(edad) && EsDecimalValido(estatura) && EsEnteroValidoDe10Digitos(telefono) && EstextoValido(nombres)
               && EstextoValido(apellidos))
            {
                //Crear una cadena con los datos 
                string datos = $"Nombres: {nombres}\r\nApellidos: {apellidos}\r\nTelefono: {telefono} \r\nEstatura: {estatura}cm\r\nEdad: {edad} años\n\nGenero: {genero}";

                //Guardar los datos en un archivo de texto
                string rutaArchivo = "C:\\documentos\\ACT_3P\\PROGRAMACIONVANZADA.txt";
                File.WriteAllText(rutaArchivo, datos);
                //Verificar si el el archcivo ya existe 
                bool archivoExiste = File.Exists(rutaArchivo);
                if (archivoExiste == false)
                {
                    File.WriteAllText(rutaArchivo, datos);
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
                    {
                        if (archivoExiste)
                        {
                            // Si el archivo existe, añadir un separador antes del nuevo regristro 
                            writer.WriteLine();
                        }
                        writer.WriteLine(datos);

                    }

                }

                //Mostrar un mensaje con los datos capturados
                MessageBox.Show("Datos guardados con exito:\n\n" + datos, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("Por favor, ingrese datos validos en los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool EsEnteroValido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);

        }

        private bool EsDecimalValido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, out resultado);

        }
        private bool EsEnteroValidoDe10Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado) && valor.Length == 10;

        }
        private bool EstextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$");

        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            tbNombre.Clear();
            tbApellidos.Clear();
            tbEdad.Clear();
            tbEstatura.Clear();
            tbTelefono.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }
        private void validarEdad(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!EsEnteroValido(textBox.Text))
            {
                MessageBox.Show("Por favor ingrese una edad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();

            }

        }

        private void validarEstatura(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!EsDecimalValido(textBox.Text))
            {
                MessageBox.Show("Por favor ingrese una estatura válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }

        }
        private void validarTelefono(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string input = textBox.Text;
            if (input.Length == 10)
            {
                if (!EsEnteroValidoDe10Digitos(input))
                {
                    MessageBox.Show("Por favor ingrese un número de teléfono válido de 10 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    textBox.Clear();
                }
            }
            else if (!EsEnteroValidoDe10Digitos(input))
            {
                MessageBox.Show("Por favor ingrese un número de teléfono válido de 10 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void validarNombre(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!EstextoValido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese un valor válido (solo letras y espacios).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }

        }
        private void validarApellidos(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!EstextoValido(textBox.Text))
            {
                MessageBox.Show("Por favor ingrese apellidos válidos (solo letras y espacios).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }





        }


    }
}