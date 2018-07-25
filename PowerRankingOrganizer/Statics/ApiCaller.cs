using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using PowerRankingOrganizer.Dtos;

/********************************************
 * Calls the Challonge API
 ********************************************/

namespace PowerRankingOrganizer.Statics
{
    public class ApiCaller
    {
        public static void DeleteTournament(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.challonge.com/v1/");
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client
                    .DeleteAsync("tournaments/"
                                 + id
                                 + ".json?api_key=#").Result;
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                    throw new HttpUnhandledException();
            }
        }

        public static List<Tournament> CallTournamentApi()
        {
            var tournaments = new List<Tournament>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.challonge.com/v1/");
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client
                    .GetAsync("tournaments.json?api_key=#").Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    var apiTournaments = JsonConvert.DeserializeObject<IEnumerable<RootTournament>>(data.Result);

                    foreach (var apiTournament in apiTournaments)
                    {
                        tournaments.Add(apiTournament.tournament);
                    }
                }
                else
                {
                    throw new HttpUnhandledException();
                }
            }
            return tournaments;
        }

        public static List<Participant> CallParticipantApi(int id)
        {
            var participants = new List<Participant>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.challonge.com/v1/");
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client
                    .GetAsync("tournaments/"
                              + id 
                              + "/participants.json?api_key=#").Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    var apiParticipants = JsonConvert.DeserializeObject<IEnumerable<RootParticipant>>(data.Result);

                    foreach (var apiParticipant in apiParticipants)
                    {
                        participants.Add(apiParticipant.participant);
                    }
                }
                else
                {
                    throw new HttpUnhandledException();
                }
            }

            return participants;
        }

        public static List<Match> CallMatchApi(int tournamentId, int participantId)
        {
            var matches = new List<Match>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.challonge.com/v1/");
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client
                    .GetAsync("tournaments/" 
                              + tournamentId 
                              + "/matches.json?api_key=#&participant_id="
                              + participantId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync();
                    var apiMatches = JsonConvert.DeserializeObject<IEnumerable<RootMatch>>(data.Result);

                    foreach (var apiMatch in apiMatches)
                    {
                        matches.Add(apiMatch.match);
                    }
                }
                else
                {
                    throw new HttpUnhandledException();
                }
            }

            return matches;
        }
    }
}