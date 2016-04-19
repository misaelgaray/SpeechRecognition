using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;


namespace VoiceRecognition
{
    public partial class Form2 : Form
    {
        SpeechRecognitionEngine recEngine;
        ABCClass abc;
        string[] edad;
        //La lista gusrda los campos que se encontraron en el speech pa' hacer el querty
        List<string> campos_list = new List<string>();
        
        public Form2()
        {
            InitializeComponent();
            abc = new ABCClass();
            
            
        }

        public string getSQL(bool var)
        {
            string sql = "";
            if (var == true) sql = "select nombre from empleados";
     
            return sql;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            button1.Enabled = false;
            button2.Enabled = true;
            txt_sql.Text = "";
            txt_natural.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Recognition
            recEngine.RecognizeAsyncStop();
            button1.Enabled = true;
            button2.Enabled = false;
            txt_sql.Text = "";
            txt_natural.Text = "";
        }

        public void  metodDelet(){

        }
        
        private void Form2_Load(object sender, EventArgs e)
        {

            recEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("es-MX"));


            edad = new string[100];
            for (int i = 0; i < edad.Length ; i++)
            {
                edad[i] = (i + 1).ToString();
            }

            /*Reglas para Select*/
            SrgsRule selectRule = new SrgsRule("selectRule");
            SrgsOneOf selectList = new SrgsOneOf(new string[]{
                "selecciona",
                "traeme",
                "busca",
                "obten",
                "muestrame",
                "muestra",
                "imprime",
                "imprimir"
            });
            selectRule.Add(selectList);

            /*Reglas para numero de registros
             * todos, los = all *
             */
            SrgsRule edadRule = new SrgsRule("edadRule");
            SrgsOneOf edadList = new SrgsOneOf(edad);
            //edadList.Add
            edadRule.Add(edadList);

            SrgsRule allRule = new SrgsRule("allRule");
            SrgsOneOf allList = new SrgsOneOf(new string[] { 
                "los",
                "todos los",
                "el nombre de los",
                "la edad de los",
                "el puesto de los",
                "el sueldo de los",
                "el sueldo y el nombre de los"
            });
            allRule.Add(allList);

            /*nombre de la tabla*/
            SrgsRule tablaRule = new SrgsRule("tablaRule");
            SrgsOneOf tablaList = new SrgsOneOf(new string[]{
                "empleados"
            });
            tablaRule.Add(tablaList);

            /*Campos de la tabla*/
            SrgsRule camposRule = new SrgsRule("camposRule");
            SrgsOneOf camposList = new SrgsOneOf(new string[]{
                    "el nombre de los",
                    "la edad de los",
                    "el puesto de los",
                    "el sueldo de los"
                });
            camposRule.Add(camposList);
        /*   SrgsRule whereRule = new SrgsRule("whereRule");
            SrgsOneOf whereList = new SrgsOneOf(new string[] { 
                ">",
                "<",
                "="
            });
            whereRule.Add(whereList);*/


            /*La referencia con una root ruke hace que que las reglas se unana y tengan que tener un 
             orden especifico, quitar o comentar la regla principal y quirar del la creacion del documenteo */
            /*Regla principal*/
            SrgsRule mainRule = new SrgsRule("mainRule");
            mainRule.Scope = SrgsRuleScope.Public;

            /*Unir los select con los all*/
            // Create the "Subject" and "Verb" rule references and add them to the SrgsDocument.
            SrgsRuleRef selectRef = new SrgsRuleRef(selectRule, "theSelect");
            mainRule.Add(selectRef);

            SrgsRuleRef allRef = new SrgsRuleRef(allRule, "theAll");
            mainRule.Add(allRef);

            SrgsRuleRef tablaRef = new SrgsRuleRef(tablaRule, "theTabla");
            mainRule.Add(tablaRef);

            
         /*  SrgsRuleRef whereRef = new SrgsRuleRef(whereRule, "theWhere");
            mainRule.Add(whereRef);*/

         /*  SrgsRuleRef edadRef = new SrgsRuleRef(edadRule, "theEdad");
            mainRule.Add(edadRef);*/

            //Creando el documento con las reglas
            SrgsDocument newDocumento = new SrgsDocument();
            newDocumento.Rules.Add(new SrgsRule[]{
              mainRule,
                selectRule,
                allRule,
                tablaRule
            });
            //newDocumento.Rules.
           newDocumento.Root = mainRule;

            //Agragar gramatica al engine
            Grammar grammar = new Grammar(newDocumento);

            recEngine.LoadGrammar(grammar);

            //Crear XML
            System.Xml.XmlWriter writer =
            System.Xml.XmlWriter.Create("C:\\Users\\Misael\\Documents\\ejemplo.xml");
            newDocumento.WriteSrgs(writer);
            writer.Close();


            // Attach a handler for the SpeechRecognized event.
            recEngine.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(recEngine_SpeechRecognized);

            // Configure the input to the SpeechRecognitionEngine object.
            recEngine.SetInputToDefaultAudioDevice();

            // Start asynchronous recognition.
            recEngine.RecognizeAsync();
        }
        
        private void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {//Engine
            string result = e.Result.Text;
            string select = "", all = "", tabla = "", campos = "";
            bool what = false;

         
            //El arreglo guarda los campos que tiene la tabla para hacer el analisis del speech
            string[] campos_array = new string[]{
            "edad",
            "nombre",
            "sueldo",
            "puesto"
            };

           
               if (e.Result.Semantics != null && e.Result.Semantics.Count != 0)
                {
                    if (e.Result.Semantics.ContainsKey("theSelect"))
                    {
                        select = e.Result.Semantics["theSelect"].Value.ToString();
                        txt_sql.Text = "SELECT ";
                        what = true;
                    }
                    else what = false;

                    if (e.Result.Semantics.ContainsKey("theAll"))
                    {
                        all = e.Result.Semantics["theAll"].Value.ToString();
                        
                            /*Revisar campos a usar por ahora solo validamos un campo*/
                            /*Para validacion ptima pero no hay tiempo, consultar los nombres de los
                             campos de la BD meterlos en la lista y hacer un loop para buscar dentro
                             dede la semantica de theAll todos los campos que se encuantren, se agregan
                             a una lista, y despues se usan para hacer el query*/
                            /*Issue: hay que agregar las reglas para todos los campos en la SrgsRULE*/
                           
                                if (all.Contains("edad"))
                                {
                                    campos_list.Add("edad");
                                }
                              /*  else if (all.Contains("sueldo") && all.Contains("nombre"))
                                {
                                    campos_list.Add("sueldo");
                                    campos_list.Add("nombre");
                                }*/
                                else if (all.Contains("sueldo"))
                                {
                                    campos_list.Add("sueldo");
                                }
                                else if (all.Contains("nombre"))
                                {
                                    campos_list.Add("nombre");
                                }
                                else if (all.Contains("puesto"))
                                {
                                    campos_list.Add("puesto");
                                }
                                else //Si no se cumple ninguna regla se opta por un all
                                {
                                    campos_list.Add("*");
                                }
                        //Concatenamos el * o el campo a buscar al txt_sql
                                foreach (var campo in campos_list)
                                {
                                    txt_sql.Text += campo+" ";
                                }
                                
                        
                        what = true;
                    }
                    else what = false;

                    if (e.Result.Semantics.ContainsKey("theTabla"))
                    {
                            tabla = e.Result.Semantics["theTabla"].Value.ToString();
                            txt_sql.Text += "FROM " + tabla+ ";";
                            what = true;
                    }
                    else what = false;

                    txt_todo.Text = result;

                    if (what)
                   {
                        txt_todo.Text = result;
                       // txt_natural.Text = select + tabla;
                       
                      //  txt_sql.Text = getSQL(what);

                        abc.startConn();

                        var emp = abc.getEmpleados(txt_sql.Text);
                        foreach (var empleado in emp)
                        {
                            list_emp.Items.Add(String.Format("{0}", empleado));
                        }
                    }
                    else MessageBox.Show("Habla Mas recio");
                    
                    
                    
            
                    
                }
               else MessageBox.Show("Habla Mas recio");
        }

        private void label1_Click(object sender, EventArgs e)
        { //Esta madre no sirve 
            MessageBox.Show("HAz hecho click");
        }

        private void list_emp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
