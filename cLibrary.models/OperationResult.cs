﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cLibrary.models
{
    public class OperationResult
    {
        private const string DefaultErrorMessage = "Si è verificato un errore imprevisto nell'elaborazione della richiesta. Contattare il supporto tecnico per maggiori informazioni.";
        private const string DefaultWarningMessage = "L'operazione non ha prodotto nessuna modifica. Contattare il supporto tecnico per maggiori informazioni.";
        public const string DefaultMessage = "Operazione completata.";
    }
}
