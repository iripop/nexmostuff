﻿using CoreProject.Requests;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace CoreProject.VoiceCallModel
{
    public static partial class Call
    {
        public class StreamCommand
        {
            /// <summary>
            /// An array of URLs pointing to the .mp3 or .wav audio file to stream.
            /// </summary>
            public string[] Stream_url { get; set; }
            /// <summary>
            /// Set to 0 to replay the audio file at stream_url when the stream ends.
            /// </summary>
            public int Loop { get; set; }
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public static CallCommandResponse BeginStream(string id, StreamCommand cmd, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("PUT", ApiRequests.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/stream"), cmd, creds);

            return JsonConvert.DeserializeObject<CallCommandResponse>(response.JsonResponse);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public static CallCommandResponse EndStream(string id, Credentials creds = null)
        {
            var response = VersionedApiRequest.DoRequest("DELETE", ApiRequests.GetBaseUriFor(typeof(Call), $"/v1/calls/{id}/stream"), new { }, creds);

            return JsonConvert.DeserializeObject<CallCommandResponse>(response.JsonResponse);
        }
    }
}
