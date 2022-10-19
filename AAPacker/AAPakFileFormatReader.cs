using System.Collections.Generic;

namespace AAPacker;

/// <summary>
/// Possible elements for Pak Header
/// </summary>
public enum AAPakFileHeaderElement
{
    /// <summary>
    /// Any byte is considered valid
    /// </summary>
    AnyByte,
    /// <summary>
    /// Byte must be zero (0x00)
    /// </summary>
    NullByte,
    /// <summary>
    /// Bytes consist of the pak file header
    /// </summary>
    Header,
    /// <summary>
    /// uint containing the file count
    /// </summary>
    FilesCount,
    /// <summary>
    /// uint containing the extra/deleted file count
    /// </summary>
    ExtraFilesCount
}

/// <summary>
/// Possible elements for File Meta data
/// </summary>
public enum AAPakFileInfoElement
{
    /// <summary>
    /// The stored file name
    /// </summary>
    FileName,
    /// <summary>
    /// Physical starting offset of the file within the pak file
    /// </summary>
    Offset,
    /// <summary>
    /// Total bytes of the stored file
    /// </summary>
    Size,
    /// <summary>
    /// Duplicate of Size, possible unpacked size
    /// </summary>
    SizeDuplicate,
    /// <summary>
    /// Number of bytes padded to the stored file to align to a block
    /// </summary>
    PaddingSize,
    /// <summary>
    /// 16 bytes containing the MD5 Hash of the stored file
    /// </summary>
    Md5,
    /// <summary>
    /// Dummy value 1
    /// </summary>
    Dummy1,
    /// <summary>
    /// long containing the stored file creation time
    /// </summary>
    CreateTime,
    /// <summary>
    /// long containing the stored file last modified time
    /// </summary>
    ModifyTime,
    /// <summary>
    /// Dummy value 2
    /// </summary>
    Dummy2,
}

/// <summary>
/// Reader class defining how file meta data should be read and written for AAPak
/// </summary>
public class AAPakFileFormatReader
{
    /// <summary>
    /// Creates a format reader object
    /// </summary>
    /// <param name="initializeWithDefaults">Set to true to populate with data that emulates the PakType.Classic format</param>
    public AAPakFileFormatReader(bool initializeWithDefaults)
    {
        // Changed default constructor to fix issues related to arrays and json populating
        if (initializeWithDefaults)
        {
            ReaderName = "Default";
            IsDefault = true;
            HeaderEncryptionKey = XlGamesKey;
            HeaderBytes = new byte[] { 0x57, 0x49, 0x42, 0x4F };
            ReadOrder = new()
            {
                AAPakFileHeaderElement.Header,
                AAPakFileHeaderElement.NullByte,
                AAPakFileHeaderElement.NullByte,
                AAPakFileHeaderElement.NullByte,
                AAPakFileHeaderElement.NullByte,
                AAPakFileHeaderElement.FilesCount,
                AAPakFileHeaderElement.ExtraFilesCount,
                AAPakFileHeaderElement.NullByte,
                AAPakFileHeaderElement.NullByte,
                AAPakFileHeaderElement.NullByte,
                AAPakFileHeaderElement.NullByte
            };
            InvertFileCounter = false;
            FileInfoReadOrder = new()
            {
                AAPakFileInfoElement.FileName,
                AAPakFileInfoElement.Offset,
                AAPakFileInfoElement.Size,
                AAPakFileInfoElement.SizeDuplicate,
                AAPakFileInfoElement.PaddingSize,
                AAPakFileInfoElement.Md5,
                AAPakFileInfoElement.Dummy1,
                AAPakFileInfoElement.CreateTime,
                AAPakFileInfoElement.ModifyTime,
                AAPakFileInfoElement.Dummy2,
            };
        }
    }

    /// <summary>
    /// Default AES128 key used by XLGames for ArcheAge as encryption key for header and fileInfo data
    /// </summary>
    public static readonly byte[] XlGamesKey = { 0x32, 0x1F, 0x2A, 0xEE, 0xAA, 0x58, 0x4A, 0xB4, 0x9A, 0x6C, 0x9E, 0x09, 0xD5, 0x9E, 0x9C, 0x6F };

    /// <summary>
    /// Name of this reader
    /// </summary>
    public string ReaderName { get; set; } = "None";

    /// <summary>
    /// Marks if this Reader has been created with initializeWithDefaults enabled
    /// </summary>
    public bool IsDefault { get; private set; }

    /// <summary>
    /// Encryption Key to use for header data
    /// </summary>
    public byte[] HeaderEncryptionKey { get; set; }

    /// <summary>
    /// Header identification bytes (4)
    /// Default for XLGames is "WIBO" (0x57 0x49 0x42 0x4F)
    /// </summary>
    public byte[] HeaderBytes { get; set; }

    /// <summary>
    /// Read order of elements for the header
    /// </summary>
    public List<AAPakFileHeaderElement> ReadOrder { get; set; }

    /// <summary>
    /// Set to true if the FAT stores Extra Files before Normal Files
    /// </summary>
    public bool InvertFileCounter { get; set; }

    /// <summary>
    /// Read order for File Info in FAT entry
    /// </summary>
    public List<AAPakFileInfoElement> FileInfoReadOrder { get; set; }

    /// <summary>
    /// Default values to use for Dummy1 on new entries
    /// </summary>
    public uint DefaultDummy1 { get; set; }

    /// <summary>
    /// Default values to use for Dummy2 on new entries
    /// </summary>
    public uint DefaultDummy2 { get; set; }
}