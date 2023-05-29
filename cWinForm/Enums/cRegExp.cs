
using System.ComponentModel;
namespace cLibrary.Enums
{
    public enum cRegExp
    {
        [StringValue("")]
        Custom,
        [StringValue("^[a-zA-Z0-9]+$")]
        [Description("Sono ammessi solo caratteri alfanumerici.")]
        TextAndNumbers,
        [StringValue("^([0-9]*)$")]
        [Description("Sono ammessi solo caratteri numerici.")]
        OnlyNumeric,
        [StringValue("^[a-zA-Z]*$")]
        [Description("Sono ammessi solo caratteri alfabetici.")]
        OnlyText,
        [StringValue(@"^\d{5}$")]
        [Description("Sono ammessi solo caratteri numerici massimo 5 cifre.")]
        Cap,
        [StringValue(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")]
        [Description("Formato della mail non corretto.")]
        Email,
        [StringValue(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$")]
        [Description("Formato data non corretto, formato richiesto 'dd,mm,yyy'.")]
        Date,
        [StringValue(@"^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$")]
        [Description("Formato ora non corretto, formato richiesto 'hh:mm'.")]
        Time
    }


}
