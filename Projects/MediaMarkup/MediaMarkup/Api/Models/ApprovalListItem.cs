using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaMarkup.Api.Models
{
    /// <summary>
    /// Approval list item details
    /// </summary>
    public class ApprovalListItem
    {
        /// <summary>
        /// Approval Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Approval Name / Description
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Approval Version Count
        /// </summary>
        [JsonProperty("versionCount")]
        public int VersionCount { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Created Date (as Unix timestamp / Epoch)
        /// </summary>
        [JsonProperty("createdUnixDate")]
        public int CreatedUnixDate { get; set; }

        /// <summary>
        /// Last Updated Date
        /// </summary>
        [JsonProperty("lastUpdatedDate")]
        public DateTime LastUpdatedDate { get; set; }

        /// <summary>
        /// Last Updated Date (as Unix timestamp / Epoch)
        /// </summary>
        [JsonProperty("lastUpdateUnixdDate")]
        public int LastUpdatedUnixDate { get; set; }

        /// <summary>
        /// Gets the latest deadline date from approval groups for the latest approval version
        /// </summary>
        [JsonProperty("deadlineDate")]
        public DateTime? DeadlineDate { get; set; }

        /// <summary>
        /// Last Updated Date (as Unix timestamp / Epoch)
        /// </summary>
        [JsonProperty("deadlineUnixDate")]
        public int DeadlineUnixDate { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// The filename of the latest approval version
        /// </summary>
        [JsonProperty("filename")]
        public string Filename { get; set; }

        /// <summary>
        /// The latest version number
        /// </summary>
        [JsonProperty("latestVersion")]
        public int LatestVersion { get; set; }

        /// <summary>
        /// The Latest Version Created Date
        /// </summary>
        [JsonProperty("latestVersionCreatedDate")]
        public DateTime? LatestVersionCreatedDate { get; set; }

        /// <summary>
        /// Created Date (as Unix timestamp / Epoch)
        /// </summary>
        [JsonProperty("latestVersionCreatedUnixDate")]
        public int LatestVersionCreatedUnixDate { get; set; }

        /// <summary>
        /// Approval Version Status Info for each Approval Group, with Status based on the built-in approval rules
        /// </summary>
        [JsonProperty("statusInfo")]
        public List<ApprovalGroupStatusInfo> GroupStatusInfo { get; set; }

        /// <summary>
        /// File width dimension of the latest approval version (if the file type is .pdf, units are mm, else px, see Units)
        /// </summary>
        [JsonProperty("width")]
        public decimal Width { get; set; }

        /// <summary>
        /// File width dimension of the latest approval version (if the file type is .pdf, units are mm, else px)
        /// </summary>
        [JsonProperty("height")]
        public decimal Height { get; set; }

        /// <summary>
        /// Units of file dimensions (referring to latest approval version)
        /// </summary>
        [JsonProperty("units")]
        public string Units { get; set; }

        /// <summary>
        /// Number of Unlocked Approval Versions
        /// </summary>
        [JsonProperty("unlockedVersionCount")]
        public int UnlockedVersionCount { get; set; }

        /// <summary>
        /// Number pages (referring to the latest approval version)
        /// </summary>
        [JsonProperty("pageCount")]
        public int PageCount { get; set; }

        /// <summary>
        /// Filename Extension (latest version)
        /// </summary>
        [JsonProperty("fileExtension")]
        public string FileExtension { get; set; }
    }
}
