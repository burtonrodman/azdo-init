using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace AzureDevOpsInit
{
    public class RepositoryOptions
    {
        [YamlMember(Alias = "account", ApplyNamingConventions = false)]
        public string AzureDevOpsCollectionUrl { get; set; }


        [YamlMember(Alias = "project", ApplyNamingConventions = false)]
        public string TeamProjectName { get; set; }


        [YamlMember(Alias = "repository", ApplyNamingConventions = false)]
        public string RepositoryName { get; set; }

        public string Version { get; set; }


        public List<BranchOptions> Branches { get; set; }

    }

    public class BranchOptions
    {
        public string Name { get; set; }

        [YamlMember(Alias = "minimum-reviewers", ApplyNamingConventions = false)]
        public int MinimumReviewers { get; set; }
        
        [YamlMember(Alias = "approve-own-changes", ApplyNamingConventions = false)]
        public bool ApproveOwnChanges { get; set; }
        
        [YamlMember(Alias = "complete-if-not-approved", ApplyNamingConventions = false)]
        public bool CompleteIfNotApproved { get; set; }
        
        [YamlMember(Alias = "reset-votes-on-new-change", ApplyNamingConventions = false)]
        public bool ResetVotesOnNewChange { get; set; }
        
        [YamlMember(Alias = "automatic-reviewers", ApplyNamingConventions = false)]
        public string[] AutomaticReviewers { get; set; }
        
        [YamlMember(Alias = "linked-work-items", ApplyNamingConventions = false)]
        public string LinkedWorkItems { get; set; }
        
        [YamlMember(Alias = "comment-resolution", ApplyNamingConventions = false)]
        public string CommentResolution { get; set; }
        
        [YamlMember(Alias = "merge-strategy", ApplyNamingConventions = false)]
        public string MergeStrategy { get; set; }

        // # build: TDB
        // # external-services: TBD

    }

}