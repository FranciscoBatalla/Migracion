﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        private readonly DL.FbatallaProgramacionNcapasContext _context;
        public Rol(DL.FbatallaProgramacionNcapasContext context)
        {
            _context = context;
        }


        public ML.Result GetAllRol()
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.RolGetAllDTOs.FromSqlRaw("RolGetAll").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        ML.Rol rol = new ML.Rol();
                        rol.IdRol = item.IdRol;
                        rol.Nombre = item.NombreRol;

                        result.Objects.Add(rol);

                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }// GETALLROL FUNCIONANDO

    }
}
