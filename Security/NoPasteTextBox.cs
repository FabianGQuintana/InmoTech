using System.Windows.Forms;

namespace InmoTech
{
    // Clase TextBox personalizada para bloquear acciones de portapapeles
    public class NoPasteTextBox : TextBox
    {
        // Códigos de mensajes de Windows
        private const int WM_PASTE = 0x0302; // Mensaje de Pegar (Paste)
        private const int WM_COPY = 0x0301;  // Mensaje de Copiar (Copy)
        private const int WM_CUT = 0x0300;   // Mensaje de Cortar (Cut)

        protected override void WndProc(ref Message m)
        {
            // Bloqueamos los mensajes de Windows para Copiar, Pegar y Cortar
            if (m.Msg == WM_PASTE || m.Msg == WM_COPY || m.Msg == WM_CUT)
            {
                // Al interceptar el mensaje y no llamar al base.WndProc,
                // prevenimos la acción de copiado/pegado/cortado.
                return;
            }

            // También podemos prevenir el menú contextual (clic derecho)
            // que a menudo contiene las opciones de copiar/pegar.
            if (m.Msg == 0x007B) // WM_CONTEXTMENU
            {
                return;
            }

            // Para cualquier otro mensaje, llamamos a la implementación base
            base.WndProc(ref m);
        }
    }
}