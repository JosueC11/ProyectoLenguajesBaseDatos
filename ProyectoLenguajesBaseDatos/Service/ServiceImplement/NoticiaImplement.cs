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

        public void SetNoticias(Noticia noticias)
        {
        }

        public void AumentarVisitas(int id)
        {
            throw new NotImplementedException();
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
    }
}
