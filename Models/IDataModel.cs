namespace Mylish.Models
{
    /// <summary>
    /// Dapperのマッピング定義インタフェース
    /// </summary>
    /// <remarks>
    /// モデルはこのインタフェースを継承してフィールドに属性([Column("XXX")])を指定すると
    /// そのフィールドは指定したカラム名(XXX)とマッピングされるようになる
    /// </remarks>
    public interface IDataModel
    {
    }
}