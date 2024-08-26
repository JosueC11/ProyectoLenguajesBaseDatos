using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Oracle.ManagedDataAccess.Client;
using ProyectoLenguajesBaseDatos.Models;
using ProyectoLenguajesBaseDatos.OracleDbContext;
using ProyectoLenguajesBaseDatos.Service.Interface;
using System.Data;

namespace ProyectoLenguajesBaseDatos.Service.ServiceImplement
{
    public class NoticiaImplement : INoticia
    {
        private readonly Context _context;
        public NoticiaImplement(Context context) 
        {
            _context = context;
        }

        public List<Noticia> GetNoticias()
        {
            var noticias = new List<Noticia>();

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_GET_NOTICIAS", connection))
                    {
                        var listaNoticias = new OracleParameter
                        {
                            ParameterName = "LISTA_NOTICIAS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaNoticias);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var noticia = new Noticia
                            {
                                IdNoticia = Convert.ToInt32(reader["ID_NOTICIA"]),
                                CorreoUsuarioCreador = reader["CORREO_USUARIO_CREADOR"].ToString(),
                                Titulo = reader["TITULO"].ToString(),
                                Sinopsis = reader["SINOPSIS"].ToString(),
                                Descripcion = reader["DESCRIPCION"].ToString(),
                                Visitas = Convert.ToInt32(reader["VISITAS"]),
                                FechaPost = Convert.ToDateTime(reader["FECHA_POST"]),
                                CalificacionPromedio = Convert.ToInt32(reader["CALIFICACION_PROMEDIO"]),
                                Tema = new Tema
                                {
                                    IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                    Titulo = reader["TITULO_TEMA"].ToString()
                                },
                                SubTema = new SubTema
                                {
                                    IdSubtema = Convert.ToInt32(reader["ID_SUBTEMA"]),
                                    Titulo = reader["TITULO_SUBTEMA"].ToString()
                                }
                            };

                            noticias.Add(noticia);
                        }
                    }
                }
                return noticias;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<Tema> GetTemas()
        {
            var temas = new List<Tema>();

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_GET_TEMAS", connection))
                    {
                        var listaTemas = new OracleParameter
                        {
                            ParameterName = "LISTA_TEMAS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaTemas);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var tema = new Tema
                            {
                                IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                Titulo = reader["TITULO"].ToString(),
                            };

                            temas.Add(tema);
                        }
                    }
                }
                return temas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        public int SetNoticias(int idTema, int idSubtema, string correo, string titulo, string sinopsis, string descripcion)
        {
            var resultado = 0;
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_PORTAL.SP_INSERTAR_NOTICIA", connection))
                    {
                        var idTemaParam = new OracleParameter
                        {
                            ParameterName = "ID_TEMA_IN",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = idTema
                        };
                        cmd.Parameters.Add(idTemaParam);

                        var idSubtemaParam = new OracleParameter
                        {
                            ParameterName = "ID_SUBTEMA_IN",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = idSubtema
                        };
                        cmd.Parameters.Add(idSubtemaParam);

                        var correoParam = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_CREADOR_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoParam);

                        var tituloParam = new OracleParameter
                        {
                            ParameterName = "TITULO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = titulo
                        };
                        cmd.Parameters.Add(tituloParam);

                        var sinopsisParam = new OracleParameter
                        {
                            ParameterName = "SINOPSIS_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = sinopsis
                        };
                        cmd.Parameters.Add(sinopsisParam);

                        var descipcionParam = new OracleParameter
                        {
                            ParameterName = "DESCRIPCION_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = descripcion
                        };
                        cmd.Parameters.Add(descipcionParam);

                        var resultadoParam = new OracleParameter
                        {
                            ParameterName = "RESULTADO",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                        resultado = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resultadoParam.Value).ToInt32();
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public int AumentarVisitas(int idNoticia)
        {
            var resultado = 0;
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_AUMENTAR_VISITA_NOTICIA", connection))
                    {
                        var idParam = new OracleParameter
                        {
                            ParameterName = "ID_NOTICIA_IN",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = idNoticia
                        };
                        cmd.Parameters.Add(idParam);

                        var resultadoParam = new OracleParameter
                        {
                            ParameterName = "RESULTADO",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                        resultado = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resultadoParam.Value).ToInt32();
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public int CalificarNoticia(int idNoticia, int calificaion, string correo)
        {
            var resultado = 0;

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_REGISTRAR_CALIFICACION_NOTICIA", connection))
                    {
                        var idNoticiaParam = new OracleParameter
                        {
                            ParameterName = "ID_NOTICIA",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = idNoticia
                        };
                        cmd.Parameters.Add(idNoticiaParam);

                        var correoParam = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoParam);

                        var calificacionParam = new OracleParameter
                        {
                            ParameterName = "CALIFICACION",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = calificaion
                        };
                        cmd.Parameters.Add(calificacionParam);

                        var resultadoParam = new OracleParameter
                        {
                            ParameterName = "RESULTADO",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                        resultado = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resultadoParam.Value).ToInt32();
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return resultado;
            }
        }

        public int ComentarNoticia(int idNoticia, string comentario, string correo)
        {
            var resultado = 0;

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_REGISTRAR_COMENTARIO_NOTICIA", connection))
                    {
                        var idNoticiaParam = new OracleParameter
                        {
                            ParameterName = "ID_NOTICIA_IN",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = idNoticia
                        };
                        cmd.Parameters.Add(idNoticiaParam);

                        var correoParam = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoParam);

                        var comentarioParam = new OracleParameter
                        {
                            ParameterName = "COMENTARIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = comentario
                        };
                        cmd.Parameters.Add(comentarioParam);

                        var resultadoParam = new OracleParameter
                        {
                            ParameterName = "RESULTADO",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                        resultado = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resultadoParam.Value).ToInt32();
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return resultado;
            }
        }

        public int CompartirNoticia(int idNoticia,string correoEnvia, string correoDestino)
        {
            var resultado = 0;

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_REGISTRAR_COMPARTIR_NOTICIA", connection))
                    {
                        var idNoticiaParam = new OracleParameter
                        {
                            ParameterName = "ID_NOTICIA_IN",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = idNoticia
                        };
                        cmd.Parameters.Add(idNoticiaParam);

                        var correoDestinoParam = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_DESTINO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correoDestino
                        };
                        cmd.Parameters.Add(correoDestinoParam);

                        var correoEnviaParam = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_DESTINO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correoEnvia
                        };
                        cmd.Parameters.Add(correoEnviaParam);

                        var resultadoParam = new OracleParameter
                        {
                            ParameterName = "RESULTADO",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                        resultado = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resultadoParam.Value).ToInt32();
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return resultado;
            }
        }

        public List<Noticia> FiltrarNoticiasTema(string filtro)
        {
            var noticiasFiltradas = new List<Noticia>();

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_GET_NOTICIAS_FILTRADAS_TEMA", connection))
                    {
                        var filtroTema = new OracleParameter
                        {
                            ParameterName = "TEMA",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = filtro
                        };
                        cmd.Parameters.Add(filtroTema);

                        var listaNoticias = new OracleParameter
                        {
                            ParameterName = "LISTA_NOTICIAS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaNoticias);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var noticia = new Noticia
                            {
                                IdNoticia = Convert.ToInt32(reader["ID_NOTICIA"]),
                                CorreoUsuarioCreador = reader["CORREO_USUARIO_CREADOR"].ToString(),
                                Titulo = reader["TITULO"].ToString(),
                                Sinopsis = reader["SINOPSIS"].ToString(),
                                Descripcion = reader["DESCRIPCION"].ToString(),
                                Visitas = Convert.ToInt32(reader["VISITAS"]),
                                FechaPost = Convert.ToDateTime(reader["FECHA_POST"]),
                                CalificacionPromedio = Convert.ToInt32(reader["CALIFICACION_PROMEDIO"]),
                                Tema = new Tema
                                {
                                    IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                    Titulo = reader["TITULO_TEMA"].ToString()
                                },
                                SubTema = new SubTema
                                {
                                    IdSubtema = Convert.ToInt32(reader["ID_SUBTEMA"]),
                                    Titulo = reader["TITULO_SUBTEMA"].ToString()
                                }
                            };

                            noticiasFiltradas.Add(noticia);
                        }
                    }
                }
                return noticiasFiltradas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<Noticia> FiltrarNoticiasCriterio(string criterio)
        {
            var noticiasFiltradas = new List<Noticia>();

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_GET_NOTICIAS_FILTRADAS_CRITERIO", connection))
                    {
                        var filtroCriterio = new OracleParameter
                        {
                            ParameterName = "CRITERIO",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = criterio
                        };
                        cmd.Parameters.Add(filtroCriterio);

                        var listaNoticias = new OracleParameter
                        {
                            ParameterName = "LISTA_NOTICIAS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaNoticias);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var noticia = new Noticia
                            {
                                IdNoticia = Convert.ToInt32(reader["ID_NOTICIA"]),
                                CorreoUsuarioCreador = reader["CORREO_USUARIO_CREADOR"].ToString(),
                                Titulo = reader["TITULO"].ToString(),
                                Sinopsis = reader["SINOPSIS"].ToString(),
                                Descripcion = reader["DESCRIPCION"].ToString(),
                                Visitas = Convert.ToInt32(reader["VISITAS"]),
                                FechaPost = Convert.ToDateTime(reader["FECHA_POST"]),
                                CalificacionPromedio = Convert.ToInt32(reader["CALIFICACION_PROMEDIO"]),
                                Tema = new Tema
                                {
                                    IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                    Titulo = reader["TITULO_TEMA"].ToString()
                                },
                                SubTema = new SubTema
                                {
                                    IdSubtema = Convert.ToInt32(reader["ID_SUBTEMA"]),
                                    Titulo = reader["TITULO_SUBTEMA"].ToString()
                                }
                            };

                            noticiasFiltradas.Add(noticia);
                        }
                    }
                }
                return noticiasFiltradas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<Noticia> FiltrarNoticiasCriterioUsuario(string criterio, string correo)
        {
            var noticiasFiltradasUsuario = new List<Noticia>();

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_PORTAL.SP_GET_NOTICIAS_FILTRADAS_CRITERIO_USUARIO", connection))
                    {
                        var correoParam = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoParam);

                        var CriterioParam = new OracleParameter
                        {
                            ParameterName = "CRITERIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = criterio
                        };
                        cmd.Parameters.Add(CriterioParam);

                        var listaNoticias = new OracleParameter
                        {
                            ParameterName = "LISTA_NOTICIAS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaNoticias);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var noticia = new Noticia
                            {
                                IdNoticia = Convert.ToInt32(reader["ID_NOTICIA"]),
                                CorreoUsuarioCreador = reader["CORREO_USUARIO_CREADOR"].ToString(),
                                Titulo = reader["TITULO"].ToString(),
                                Sinopsis = reader["SINOPSIS"].ToString(),
                                Descripcion = reader["DESCRIPCION"].ToString(),
                                Visitas = Convert.ToInt32(reader["VISITAS"]),
                                FechaPost = Convert.ToDateTime(reader["FECHA_POST"]),
                                CalificacionPromedio = Convert.ToInt32(reader["CALIFICACION_PROMEDIO"]),
                                Tema = new Tema
                                {
                                    IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                    Titulo = reader["TITULO_TEMA"].ToString()
                                },
                                SubTema = new SubTema
                                {
                                    IdSubtema = Convert.ToInt32(reader["ID_SUBTEMA"]),
                                    Titulo = reader["TITULO_SUBTEMA"].ToString()
                                }
                            };

                            noticiasFiltradasUsuario.Add(noticia);
                        }
                    }
                }
                return noticiasFiltradasUsuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public Noticia VerNoticia(int idNoticia)
        {
            var noticiaReady = new Noticia();
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_PORTAL.SP_VER_NOTICIA", connection))
                    {
                        var idParam = new OracleParameter
                        {
                            ParameterName = "ID_NOTICIA_IN",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = idNoticia
                        };
                        cmd.Parameters.Add(idParam);

                        var noticiaParam = new OracleParameter
                        {
                            ParameterName = "NOTICIA",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(noticiaParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            noticiaReady.IdNoticia = Convert.ToInt32(reader["ID_NOTICIA"]);
                            noticiaReady.CorreoUsuarioCreador = reader["CORREO_USUARIO_CREADOR"].ToString();
                            noticiaReady.Titulo = reader["TITULO"].ToString();
                            noticiaReady.Sinopsis = reader["SINOPSIS"].ToString();
                            noticiaReady.Descripcion = reader["DESCRIPCION"].ToString();
                            noticiaReady.Visitas = Convert.ToInt32(reader["VISITAS"]);
                            noticiaReady.FechaPost = Convert.ToDateTime(reader["FECHA_POST"]);
                            noticiaReady.CalificacionPromedio = Convert.ToInt32(reader["CALIFICACION_PROMEDIO"]);
                            noticiaReady.Tema = new Tema
                            {
                                IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                Titulo = reader["TITULO_TEMA"].ToString()
                            };
                            noticiaReady.SubTema = new SubTema
                            {
                                IdSubtema = Convert.ToInt32(reader["ID_SUBTEMA"]),
                                Titulo = reader["TITULO_SUBTEMA"].ToString()
                            };
                        }
                    }
                }
                return noticiaReady;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<Noticia> GetNoticiasRecientes()
        {
            var noticias = new List<Noticia>();

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_GET_NOTICIAS_RECIENTES", connection))
                    {
                        var listaNoticias = new OracleParameter
                        {
                            ParameterName = "LISTA_NOTICIAS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaNoticias);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var noticia = new Noticia
                            {
                                IdNoticia = Convert.ToInt32(reader["ID_NOTICIA"]),
                                CorreoUsuarioCreador = reader["CORREO_USUARIO_CREADOR"].ToString(),
                                Titulo = reader["TITULO"].ToString(),
                                Sinopsis = reader["SINOPSIS"].ToString(),
                                Descripcion = reader["DESCRIPCION"].ToString(),
                                Visitas = Convert.ToInt32(reader["VISITAS"]),
                                FechaPost = Convert.ToDateTime(reader["FECHA_POST"]),
                                CalificacionPromedio = Convert.ToInt32(reader["CALIFICACION_PROMEDIO"]),
                                Tema = new Tema
                                {
                                    IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                    Titulo = reader["TITULO_TEMA"].ToString()
                                },
                                SubTema = new SubTema
                                {
                                    IdSubtema = Convert.ToInt32(reader["ID_SUBTEMA"]),
                                    Titulo = reader["TITULO_SUBTEMA"].ToString()
                                }
                            };

                            noticias.Add(noticia);
                        }
                    }
                }
                return noticias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<Noticia> GetNoticiasUsuario(string correo)
        {
            var noticias = new List<Noticia>();
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_PORTAL.SP_GET_NOTICIAS_USUARIO", connection))
                    {
                        var correoParam = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoParam);

                        var listaNoticias = new OracleParameter
                        {
                            ParameterName = "LISTA_NOTICIAS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaNoticias);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var noticia = new Noticia
                            {
                                IdNoticia = Convert.ToInt32(reader["ID_NOTICIA"]),
                                CorreoUsuarioCreador = reader["CORREO_USUARIO_CREADOR"].ToString(),
                                Titulo = reader["TITULO"].ToString(),
                                Sinopsis = reader["SINOPSIS"].ToString(),
                                Descripcion = reader["DESCRIPCION"].ToString(),
                                Visitas = Convert.ToInt32(reader["VISITAS"]),
                                FechaPost = Convert.ToDateTime(reader["FECHA_POST"]),
                                CalificacionPromedio = Convert.ToInt32(reader["CALIFICACION_PROMEDIO"]),
                                Tema = new Tema
                                {
                                    IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                    Titulo = reader["TITULO_TEMA"].ToString()
                                },
                                SubTema = new SubTema
                                {
                                    IdSubtema = Convert.ToInt32(reader["ID_SUBTEMA"]),
                                    Titulo = reader["TITULO_SUBTEMA"].ToString()
                                }
                            };

                            noticias.Add(noticia);
                        }
                    }
                }
                return noticias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<Noticia> FiltrarPalabra(string filtro)
        {
            var noticias = new List<Noticia>();
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_PORTAL.SP_FILTRAR_PALABRA", connection))
                    {
                        var filtroParam = new OracleParameter
                        {
                            ParameterName = "FILTRO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = filtro
                        };
                        cmd.Parameters.Add(filtroParam);

                        var listaNoticias = new OracleParameter
                        {
                            ParameterName = "LISTA_NOTICIAS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaNoticias);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var noticia = new Noticia
                            {
                                IdNoticia = Convert.ToInt32(reader["ID_NOTICIA"]),
                                CorreoUsuarioCreador = reader["CORREO_USUARIO_CREADOR"].ToString(),
                                Titulo = reader["TITULO"].ToString(),
                                Sinopsis = reader["SINOPSIS"].ToString(),
                                Descripcion = reader["DESCRIPCION"].ToString(),
                                Visitas = Convert.ToInt32(reader["VISITAS"]),
                                FechaPost = Convert.ToDateTime(reader["FECHA_POST"]),
                                CalificacionPromedio = Convert.ToInt32(reader["CALIFICACION_PROMEDIO"]),
                                Tema = new Tema
                                {
                                    IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                    Titulo = reader["TITULO_TEMA"].ToString()
                                },
                                SubTema = new SubTema
                                {
                                    IdSubtema = Convert.ToInt32(reader["ID_SUBTEMA"]),
                                    Titulo = reader["TITULO_SUBTEMA"].ToString()
                                }
                            };

                            noticias.Add(noticia);
                        }
                    }
                }
                return noticias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<Comentario> GetComentarios(int idNoticia)
        {
            var comentarios = new List<Comentario>();
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_PORTAL.SP_GET_COMENTARIOS_NOTICIAS", connection))
                    {
                        var idNoticiaParam = new OracleParameter
                        {
                            ParameterName = "ID_NOTICIA_IN",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = idNoticia
                        };
                        cmd.Parameters.Add(idNoticiaParam);

                        var listaComentarios = new OracleParameter
                        {
                            ParameterName = "LISTA_COMENTARIOS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaComentarios);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var comentario = new Comentario
                            {
                                CorreoUsuario = reader["CORREO_USUARIO"].ToString(),
                                StringComentario = reader["COMENTARIO"].ToString(),

                            };
                            comentarios.Add(comentario);
                        }
                    }
                }
                return comentarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }       
    }
}
