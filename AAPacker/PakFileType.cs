namespace AAPacker;

/// <summary>
/// Type of pak file currently used
/// </summary>
public enum PakFileType
{
    /// <summary>
    /// FormatReader back pak file
    /// </summary>
    Reader,
    /// <summary>
    /// Classic "normal" pak file
    /// </summary>
    Classic,
    /// <summary>
    /// Virtual pak file made from CSV data
    /// </summary>
    Csv,
}