using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculadoraMovil_SLE
{
    public partial class MainPage : ContentPage
    {
        //HACIENDO VARIABLES GLOBALES PARA PONER EN LOS DIFERENTES METODOS
        private double _numeroOperador1 = 0; //ESTE ES EL PRIMER NUMERO QUE TENGO QUE AÑADIR (POR EJEMPLO A)
        private double _numeroOperador2 = 0; //ESTE ES EL SEGUNDO NUMERO QUE SE USARA EN LAS OPERACIONES (B POR EJEMPLO)
        //SE DEBE DE USAR STRING YA QUE LOS ANTERIORES (LOS DOUBLES) SE USARAN PARA HACER LAS DIFERENTES OPERACIONES EN LOS METODOS
        //PERO LUEGO TENGO QUE PASAR ESE DOBLE A STRING PARA MOSTRALO EN PANTALLA 
        private string _operadorTexto1 = ""; //ESTE SE UTILIZA PARA GUARDAR EL NUMEROOPERADOR1 EN UN STRING Y MOSTRALO A PANTALLA
        private string _operadorTexto2 = ""; //LO MISMO PERO PARA GUARDAR EL SEGUNDO OPERADOR
        private string _resultado = "";
        private string _operadorSigno = ""; //ESTE SE USA PARA QUE LEEA SI ES MAS, MENOS, POR ,ETC
        public MainPage()
        {
            InitializeComponent();
        }

        private void Limpiar(object sender, EventArgs e)
        {
            /* UTILIZO TODAS MIS VARIABLES GLOBALES QUE USE AL FONDO PARA QUE UNA VEZ QUE EL USUARIO LE PONGA EN LIMPIAR TODO, POS
              TODO DEBE DE REGRESAR A COMO ESTABA, POR ESO LAS VARIABLES ESTAN INICIALIZADAS VACIAS
             */
            _numeroOperador1 = 0;
            _numeroOperador2 = 0;
            _operadorTexto1 = "";
            _operadorTexto2 = "";
            _resultado = "";
            _operadorSigno = "";
            //LLAMANDO A MI "PANTALLA" DONDE SALE EL RESULTADO PS
            Pantalla.Text = "";



        }

        private void numeros(object sender, EventArgs e)
        {
            // SE LE INDICA AL SENDER QUE EL EVENTARGS ES UN BOTÓN
            Button boton = sender as Button;

            // EN CASO DE QUE EL USUARIO HAYA PICADO EN EL OPERADOR PERO TODAVÍA NO ELIGE EL OTRO NÚMERO
            // ENTONCES SE DEBE DE GUARDAR LA VARIABLE O EL NÚMERO EN OPERADORTEXTO1 PARA QUE NO SE MODIFIQUE
            if (_operadorSigno == "" && _operadorTexto2 == "")
            {
                _operadorTexto1 = _operadorTexto1 + (string)boton.Text;
                // SE LEE O SE GUARDA EL BOTÓN QUE APLASTA EL USUARIO Y SE CONCATENA PARA QUE PERMITA MÁS DE UN VALOR
                // AHORA SE DEBE DE CONVERTIR A NÚMERO EL TEXTO PARA SU MANIPULACIÓN
                _numeroOperador1 = double.Parse(_operadorTexto1);

                // SE LLAMA AL MÉTODO DE MOSTRAR PANTALLA  Y SE LE PASA EL VALOR
                MostrarPantalla(_operadorTexto1);
            }
            // EN CASO DE QUE NO SEA ASÍ, SE DEBE MANEJAR LOS VALORES DEL SEGUNDO NÚMERO O OPERADOR
            else
            {
                //SE COMPRUEBA QUE EN EL TEXTO NO HAIGA UN PUNTO EN CASO DE QUE SI HAIGA, SE RETORNA VACIO PARA Q NO SE PONGA NADA SIUUUU
                if (boton.Text == "." && _operadorTexto2.Contains("."))
                {
                   
                    return;
                }

                _operadorTexto2 = _operadorTexto2 + (string)boton.Text;
                _numeroOperador2 = double.Parse(_operadorTexto2);

                // SE LLAMA AL MÉTODO DE MOSTRAR PANTALLA  Y SE LE PASA EL VALOR
                MostrarPantalla(_numeroOperador1, _numeroOperador2);
            }
        }


        private void punto(object sender, EventArgs e)
        {
            //SE COMPREEBA QUE SE PUSO UN SIGNO Y QUE TAMBIEN EL SEGUNDO NUMERO NO DEBE DE TENER  UN PUNTO
            if (_operadorSigno != "" && !_operadorTexto2.Contains("."))
            {
                //SE PONE EL PUNTITO AL NUMERO 2 
                _operadorTexto2 += ".";
                // Actualizar la pantalla con los dos números y el operador.
                MostrarPantalla(_operadorTexto1 + " " + _operadorSigno + " " + _operadorTexto2);
            }
            // Si no se ha ingresado un operador o si el segundo número ya contiene un punto, verificar el primer número y si no contiene un punto.
            else if (_operadorSigno == "" && !_operadorTexto1.Contains("."))
            {
                // Agregar un punto al primer número.
                _operadorTexto1 += ".";
                // Actualizar la pantalla solo con el primer número.
                MostrarPantalla(_operadorTexto1);
            }
        }

        private void operador(object sender, EventArgs e)
        {
            //SE LE INDICA AL SENDER QUE EL EVENTARGS ES UN BOTON
            Button boton = sender as Button;
            //
            if (_operadorSigno == "" && _operadorTexto2 == "")
            {
                //EN CASO DE QUE NO HAYA VALOR O NO EXISTA O NO SE HAYA APLASTADO EL BOTON, CON ESTO LE AÑADIMOS VALOR  
                _operadorSigno = (string)boton.Text;

                MostrarPantalla(_operadorTexto1);
            }
        }
        private void igual(object sender, EventArgs e)
        {
            //SE CHECA SI EL USUARIO PUSO O NO UN SIGNO Y TAMBIEN NUMEROS
            if (!string.IsNullOrEmpty(_operadorSigno) && !string.IsNullOrEmpty(_operadorTexto2))
            {
                switch (_operadorSigno)
                {
                    case "+":
                        _resultado = Convert.ToString(_numeroOperador1 + _numeroOperador2);
                        break;
                    case "-":
                        _resultado = Convert.ToString(_numeroOperador1 - _numeroOperador2);
                        break;
                    case "x":
                        _resultado = Convert.ToString(_numeroOperador1 * _numeroOperador2);
                        break;
                    case "%":
                        _resultado = Convert.ToString((_numeroOperador1 * _numeroOperador2) / 100);
                        break;
                    case "÷":
                        //SE COMPRUEBA QUE EL SEGUNDO NUMERO NO SEA UN 0 YA QUE NO SE PUEDE DIVIDIR ENTRE 0
                        if (_numeroOperador2 == 0)
                        {
                            _resultado = "IIIIIIIJOLE YO CREO Q NO SE VA A PODER";
                        }
                        else
                        {
                            _resultado = Convert.ToString(_numeroOperador1 / _numeroOperador2);
                        }
                        break;
                }
            }

            else
            {
               //SE PONE UN 0 (POR Q SI) EN CASO DE QUE EL USUARIO NO AIGA AÑADIDO NUMERO Y/O OPERADOR
                _resultado = "0";
                MostrarPantalla(_resultado);
            }

                //PARA QUE PERMITA HACER MAS OPERACIONES SEGUIDAS
                //SE IGUALA EL RESULTADO O MEJOR DICHO SE GUARDA PARA TENER LIBRE LA VARIABLE Y RENICIARLA LUEGO
                _operadorTexto1 = _resultado;
            //SE GUARDA Y SE CONVIERTE PARA Q SE PUEDA MOSTRAR EN PANTALLA
            _numeroOperador1 = double.Parse(_operadorTexto1);

            //SE RENICIA EL OTRO NUMERO, YA QUE EN TEORIA YA SE TUVO QUE HABER GUADADO EL RESULTADO
            _operadorTexto2 = "";
            _numeroOperador2 = 0; //SE RENICIA TAMBIEN EL NUMERO
            _operadorSigno = ""; //TAMBIEN SE DEBE DE RENICIAR EL SIGNO
            _resultado = ""; //x2

            MostrarPantalla(_operadorTexto1, _operadorTexto2);
        }



        private void LimpiarUno(object sender, EventArgs e)
        {
            //SE COMPRUEBA QUE NO HAIGA RESULTADO, YA QUE POS NO SE PUEDE BORRAR EL RESULTADO PS
            if (_resultado == "")
            {
                //SE COMPRUEBA QUE NO HAIGA SIGNO
                if (_operadorSigno == "")
                {
                    // SE COMPRUEBA PRIMERO QUE LA LONGITUD SEA MAS DE UN NUMERO, POS PA Q NO TRUENE
                    if (_operadorTexto1.Length > 0)
                    {
                        //SE BUSCA EL ULTIMO LETRA O EN ESTE CASO NUMERO PARA ELIMINAR UNO EN UNO
                        _operadorTexto1 = _operadorTexto1.Substring(0, _operadorTexto1.Length - 1);
                        _numeroOperador1 = _operadorTexto1.Length > 0
                            ? double.TryParse(_operadorTexto1, out double parsedValue) ? parsedValue : 0
                            : 0;
                        MostrarPantalla(_operadorTexto1);
                    }
                }
                else
                {
                    //AHORA PARA SABER CON EL OTRO NUMERO, SE HACE LO MIOSMO QUE CON EL PRIMERO
                    if (_operadorTexto2.Length > 0)
                    {
                       
                        _operadorTexto2 = _operadorTexto2.Substring(0, _operadorTexto2.Length - 1);
                        _numeroOperador2 = _operadorTexto2.Length > 0
                            ? double.TryParse(_operadorTexto2, out double parsedValue) ? parsedValue : 0
                            : 0;
                        MostrarPantalla(_operadorTexto1 + " " + _operadorSigno + " " + _operadorTexto2);
                    }
                    else
                    {
                        
                        _operadorSigno = "";
                        MostrarPantalla(_operadorTexto1);
                    }
                }
            }
        }



        private void MostrarPantalla(Object numero1, Object numero2 = null) //EL NUMERO2 PUEDE SER NULO POR QUE PUEDE SER QUE SOLO PONGAS UN NUMERO
        {
            //SE HACE UN IF QUE EN CASO DE QUE NO HAYA RESULTADO SE HACE UNA COSA
            if (_resultado == "")
            {
                /*ESTE IF QUE ESTA ADENTRO DEL OTRO IF (YA QUE EL OTRO ENTRA CUANDO AUN NO LE DAS EL IGUAL) ESTE ES CUANDO
                 TODAVIA NO PONES UN SIGNO, EL NUMERO1 ES EL NUMERO Q INTRODUCCES POR PRIMERA VEZ*/
                if (_operadorSigno == "")

                {
                    Pantalla.Text = Convert.ToString(numero1);
                }
                /*UNA VEZ QUE LE HAYAS PICADO EN EL SIGNO (+ POR EJEMPLO) YA TE IMPRIMIRA EL PRIMER NUMERO QUE PUSISTES
                 MAS EL SIGNO (+) MAS EL OTRO NUMERO QUE HAYAS PUESTO */
                else
                {
                    Pantalla.Text = Convert.ToString(numero1) + _operadorSigno + Convert.ToString(numero2);
                }


            }
            //Y EN CASO DE QUE SI HAY RESULTADO(QUE ES CUANDO LE DAS ENTRER) TE DEBE DE MOSTRAR EL RESULTADO EN PANTALLA
            else
            {
                //AKI ONDE TE IMPRIME LA PANTALLA CON LA VARIABLE _RESULTADO (NO SE CONVIERTE POR QUE YA ES UN STRING)
                Pantalla.Text =  _resultado;
            }
        }

        async void Secreto(object sender, EventArgs e)
        {
            

            string tema = "uwu";
            string mensaje = "Hey no seas curioso, yo no hago nada mas que darte este mensaje que consume recursos gratis:D";
            string quitar = "Lo siento, soy un curioso, prometo no hacerlo otra vez";

            await DisplayAlert(tema, mensaje, quitar);
        }

    }
}
