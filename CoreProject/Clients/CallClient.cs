using Nexmo.Api.Request;
using static Nexmo.Api.Voice.Call;

namespace CoreProject.Clients
{
    public class CallClient
    {
        public Credentials Credentials { get; set; }

        public CallClient(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public CallResponse Do(CallCommand cmd, Credentials creds = null)
        {
            return Do(cmd, creds ?? Credentials);
        }

        /// <summary>
        /// GET /v1/calls - retrieve information about all your Calls
        /// <param name="filter">Filter to search calls on</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// </summary>
        public PaginatedResponse<CallList> List(SearchFilter filter, Credentials creds = null)
        {
            return List(filter, creds ?? Credentials);
        }

        public PaginatedResponse<CallList> List(Credentials creds = null)
        {
            return List(new SearchFilter
            {
                page_size = 10
            }, creds ?? Credentials);
        }

        /// <summary>
        /// GET /v1/calls/{uuid} - retrieve information about a single Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public CallResponse Get(string id, Credentials creds = null)
        {
            return Get(id, creds ?? Credentials);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid} - modify an existing Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public CallResponse Edit(string id, CallEditCommand cmd, Credentials creds = null)
        {
            return Edit(id, cmd, creds ?? Credentials);
        }

        #region Stream

        /// <summary>
        /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public CallCommandResponse BeginStream(string id, StreamCommand cmd, Credentials creds = null)
        {
            return BeginStream(id, cmd, creds ?? Credentials);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public CallCommandResponse EndStream(string id, Credentials creds = null)
        {
            return EndStream(id, creds ?? Credentials);
        }

        #endregion

        #region Talk
        /// <summary>
        /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public CallCommandResponse BeginTalk(string id, TalkCommand cmd, Credentials creds = null)
        {
            return BeginTalk(id, cmd, creds ?? Credentials);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public CallCommandResponse EndTalk(string id, Credentials creds = null)
        {
            return EndTalk(id, creds ?? Credentials);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        public CallCommandResponse SendDtmf(string id, DtmfCommand cmd, Credentials creds = null)
        {
            return SendDtmf(id, cmd, creds ?? Credentials);
        }
        #endregion
    }
}
