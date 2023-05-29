
namespace cLibrary.Enums
{
    public enum eFileFilter
    {
        [StringValue("")]
        None,
        [StringValue("Image|*.png;*jpg;*jpeg;*gif")]
        Image,
        [StringValue("Excel Files|*.xls;*.xlsx;*.xlsb;*.xlsm")]
        Excel,
        [StringValue("Word Files|*.doc;*.docx;")]
        Word,
        [StringValue("Text Files|*.txt")]
        Text,
        [StringValue("Xml Files|*.xml")]
        Xml
    }
}
