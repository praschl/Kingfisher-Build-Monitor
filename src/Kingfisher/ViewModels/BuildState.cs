namespace Kingfisher.ViewModels
{
    /// These map to either Kingfisher.Provider.Builds.Build.State or Kingfisher.Provider.Builds.Build.Result
    /// first its tried to map to a result in <see cref="BuildsOverviewViewModel.GetVmBuildStatus"/> and
    /// if that fails, tries to map to the state. If that fails, too, its set to unknown.
    public enum BuildState
    {
        unknown,
        // states
        notStarted,
        postponed,
        inProgress,
        cancelling,
        // results
        succeeded,
        partiallySucceeded,
        canceled,
        failed,
        // the completed state is here to allow for simpler code when parsing.
        // when the state is completed, it will actually be set to one of the results.
        completed,

    }
}
