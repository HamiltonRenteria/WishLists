﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Static
{
    public static class ReplyMessage
    {
        public const string MESSAGE_QUERY = "Consulta exitosa.";
        public const string MESSAGE_QUERY_EMPTY = "No se encontraron registros.";
        public const string MESSAGE_TOKEN = "Token generado correctamente.";
        public const string MESSAGE_TOKEN_ERROR = "Usuario y/o contraseña es incorrecta.";

        // CRUD
        public const string MESSAGE_SAVE = "Se registró correctamente.";
        public const string MESSAGE_UPDATE = "Se actualizó correctamente.";
        public const string MESSAGE_DELETE = "Se eliminó correctamente.";
        public const string MESSAGE_EXISTS = "El registro ya existe.";
        public const string MESSAGE_FAILED = "Operación fallida.";

        public const string MESSAGE_VALIDATE = "Se encontraron errores.";
    }
}
