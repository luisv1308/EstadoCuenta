namespace EstadoCuenta.Web.Models
{
    public class ResultadoOperacion
    {
        public bool Exitoso { get; private set; }
        public List<string> Errores { get; private set; } = new List<string>();

        private ResultadoOperacion(bool exitoso, List<string>? errores = null)
        {
            Exitoso = exitoso;
            if (errores != null)
            {
                Errores = errores;
            }
        }

        public static ResultadoOperacion Ok() => new ResultadoOperacion(true);
        public static ResultadoOperacion Fallo(params string[] errores) => new ResultadoOperacion(false, errores.ToList());
        public static ResultadoOperacion Fallo(List<string> errores) => new ResultadoOperacion(false, errores);
    }
}
