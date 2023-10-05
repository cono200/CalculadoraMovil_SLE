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
            //SE LE INDICA AL SENDER QUE EL EVENTARGS ES UN BOTON
            Button boton = sender as Button;

            //EN CASO DE QUE EL USUARIO HAYA PICADO EN EL OPERADOR PERO TODAVIA NO ELIGE EL OTRO NUMERO
            //ENTOCES SE DEBE DE GUARDAR LA VARIABLE O EL NUMERO EN OPERADORTEXTO1 PARA QUE NO SE MODIFIQUE
            if (_operadorSigno == "" && _operadorTexto2 == "")
            {
                _operadorTexto1 = _operadorTexto1 + (string)boton.Text;  //SE LEE O SE GUARDA EL BOTON QUE APLASTA EL USUARIO Y SE CONTATENA PARA QUE PERMITA MAS DE UN VALOR
                                                                         //AHORA SE DEBE DE CONVERTIR A NUMERO EL TEXTO PARA SU MANIPULACION
                _numeroOperador1 = double.Parse(_operadorTexto1);


                //SE LLAMA AL METODO DE MOSTRAR PANTALLA  Y SE LE PASA EL VALOR
                MostrarPantalla(_operadorTexto1);

            }
            //EN CASO DE QUE NO SEA ASI, POS SE TIENE QUE AHORA COMO ASEGURAR LOS VALORES QUE TIENE EL SEGUNDO NUMERO O OPERACION
            else
            {
                _operadorTexto2 = _operadorTexto2 + (string)boton.Text;
                _numeroOperador2 = Convert.ToInt32(_operadorTexto2);


                //SE LLAMA AL METODO DE MOSTRAR PANTALLA  Y SE LE PASA EL VALOR
                MostrarPantalla(_numeroOperador1, _numeroOperador2);
            }







        }

        private void punto(object sender, EventArgs e)
        {
            //SE DEBE DE UTILIZAR EL STRING DE OPERADORTEXTO PARA QUE EN PANTALLA
            //SE MUESTRE EL PUNTO
            //SE NECESITA QUE HAYA UN NUMERO ANTERIORMENTE PARA QUE DIBUJE EL PUNTO
            //TAMBIEN SE COMPRUEBA QUE NO HAYA SIGNO PRIMERO
            if (_operadorSigno == "" && _operadorTexto1 != "" && _numeroOperador1 % 1 == 0)
            {
                _operadorTexto1 = _operadorTexto1 + ".";
                //SE VUELVE A LLAMAR EL METODO QUE IMPRIME TODO
                MostrarPantalla(_operadorTexto1);
            }
            //LO MISMO PERO PARA EL OTRO NUMERO O OPERADOR
            if (_operadorSigno == "" && _operadorTexto2 != "" && _numeroOperador2 % 1 == 0)
            {
                _operadorTexto2 = _operadorTexto2 + ".";
                //SE VUELVE A LLAMAR EL METODO QUE IMPRIME TODO
                MostrarPantalla(_operadorTexto1, _operadorTexto2);
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
            //SE COMPRUEBA QUE REALMENTE LE APLASTARON EL IGUAL
            if (_operadorSigno != "")
            {
                //YA SE HACE LAS OPERACIONES NORMALES CON UN SWITCH

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
                        _resultado = Convert.ToString((_numeroOperador1 / _numeroOperador2) * 100);
                        break;
                    case "÷":
                        _resultado = Convert.ToString(_numeroOperador1 / _numeroOperador2);
                        break;
                }
            }

            MostrarPantalla(_operadorTexto1, _operadorTexto2);
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




        //HACIENDO VARIABLES GLOBALES PARA PONER EN LOS DIFERENTES METODOS
        private double _numeroOperador1 = 0; //ESTE ES EL PRIMER NUMERO QUE TENGO QUE AÑADIR (POR EJEMPLO A)
        private double _numeroOperador2 = 0; //ESTE ES EL SEGUNDO NUMERO QUE SE USARA EN LAS OPERACIONES (B POR EJEMPLO)
        //SE DEBE DE USAR STRING YA QUE LOS ANTERIORES (LOS DOUBLES) SE USARAN PARA HACER LAS DIFERENTES OPERACIONES EN LOS METODOS
        //PERO LUEGO TENGO QUE PASAR ESE DOBLE A STRING PARA MOSTRALO EN PANTALLA 
        private string _operadorTexto1 = ""; //ESTE SE UTILIZA PARA GUARDAR EL NUMEROOPERADOR1 EN UN STRING Y MOSTRALO A PANTALLA
        private string _operadorTexto2 = ""; //LO MISMO PERO PARA GUARDAR EL SEGUNDO OPERADOR
        private string _resultado = "";
        private string _operadorSigno = ""; //ESTE SE USA PARA QUE LEEA SI ES MAS, MENOS, POR ,ETC
    }
}
