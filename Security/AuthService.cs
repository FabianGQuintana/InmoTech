using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InmoTech.Models;

namespace InmoTech.Security
{
    public static class AuthService
    {
        public static Usuario? CurrentUser { get; private set; }

        public static bool IsAuthenticated => CurrentUser != null;

        public static void SignIn(Usuario usuario)
        {
            CurrentUser = usuario;
        }

        public static void SignOut()
        {
            CurrentUser = null;
        }

        public static bool IsInRole(int idRol) => CurrentUser?.IdRol == idRol;
        // Si luego querés por nombre de rol, podés mapear Roles en otro servicio o traer join.
    }
}
