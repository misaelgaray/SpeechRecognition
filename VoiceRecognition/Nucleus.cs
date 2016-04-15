using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace VoiceRecognition
{
    
    class Nucleus
    {
        
       
        /*Tipo de peticiones ej. Traeme, busca, */
        private List<string> peticiones;
        /*Numbers*/
        private List<int> numbers;

        public Nucleus()
        {

        }
        public static Grammar getGrammar()
        {
            GrammarBuilder builder = new GrammarBuilder();
            builder.AppendRuleReference("file://c:/users/misael/documents/visual studio 2013/Projects/VoiceRecognition/VoiceRecognition/semantic.grxml");
            

            Grammar returnGrammar = new Grammar(builder);
            returnGrammar.Name = "Semantic";

            return returnGrammar;
        }
    }
}
