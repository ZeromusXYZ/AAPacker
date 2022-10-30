namespace AAPacker;

/// <summary>
/// Event to handle progress
/// </summary>
/// <param name="sender">Event origin AAPak object</param>
/// <param name="progressType">Progress type</param>
/// <param name="step">Current step</param>
/// <param name="maximum">Maximum amount of steps for the current progress type action</param>
public delegate void AAPakNotify(AAPak sender, AAPakLoadingProgressType progressType, int step, int maximum);

/// <summary>
/// Enumerator used by OnProgress
/// </summary>
public enum AAPakLoadingProgressType
{
    /// <summary>
    /// Currently opening the pak file
    /// </summary>
    OpeningFile,
    /// <summary>
    /// Currently reading the pak file header
    /// </summary>
    ReadingHeader,
    /// <summary>
    /// Currently writing the pak file header
    /// </summary>
    WritingHeader,
    /// <summary>
    /// Currently reading stored file meta data
    /// </summary>
    ReadingFAT,
    /// <summary>
    /// Currently writing stored file meta data
    /// </summary>
    WritingFAT,
    /// <summary>
    /// Currently closing the pak file
    /// </summary>
    ClosingFile,
    /// <summary>
    /// Currently generating virtual directories from stored files list
    /// </summary>
    GeneratingDirectories,
}