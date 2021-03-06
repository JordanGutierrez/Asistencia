﻿using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IJustificacionDAO
    {
        List<Justificacion> getAllJustificacion(ref string mensaje);

        Justificacion getJustificacionByAsistencia(int asistenciaid, ref string mensaje);

        void insertJustificacion(Justificacion justificacion, string user, ref string mensaje);

        void insertJustificacionAtraso(Justificacion justificacion, string user, ref string mensaje);
    }
}
