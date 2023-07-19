using System.ComponentModel;

namespace CRUDAPINETSix.Enums
{
    public enum StatusTarefa
    {
        [Description("A fazer")]
        AFazer =1,
        [Description("Em andamaneto")]
        EmAndamento = 2,
        [Description("Concluído")]
        Concluido = 3
    }
}
