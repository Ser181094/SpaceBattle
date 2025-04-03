using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json.Linq;

namespace SpaceBattle.Auth
{
    public class AuthenticationService
    {
        public Dictionary<int, int[]> StarBattles { get; }

        private Dictionary<int, string> Users { get; set; }

        public AuthenticationService()
        {
            this.StarBattles = new Dictionary<int, int[]>();
            this.Users = new Dictionary<int, string>
            {
                { 1, "FirstUser" },
                { 2, "SecondUser" },
                { 3, "ThirdUser" },
                { 4, "FourthUser" },
                { 5, "FifthUser" },
            };
        }

        public int SpaceBattleRegister(string jwtToken, int[] battleUserIds)
        {
            if (!ValidateToken(jwtToken)) return 0;
            var battleId = this.StarBattles.Count + 1;
            this.StarBattles.Add(battleId, battleUserIds);
            return battleId;
        }

        public string GetUserAuthenticationJwt(int userId, string userName)
        {
            try
            {
                if (this.Users.TryGetValue(userId, out var name) && name == userName)
                {
                    return JwtBuilder.Create().WithAlgorithm(new NoneAlgorithm()).AddClaim("userId", userId).Encode();
                }
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        public string GetStarBattleAuthorizationJwt(int battleId, string token)
        {
            try
            {
                dynamic parsedToken = JObject.Parse(JwtBuilder.Create().WithAlgorithm(new NoneAlgorithm()).Decode(token));

                if (this.StarBattles.TryGetValue(battleId, out var battleUserIds) &&
                    battleUserIds.Contains((int)parsedToken.userId))
                {
                    return JwtBuilder.Create().WithAlgorithm(new NoneAlgorithm()).AddClaim("userId", (int)parsedToken.userId)
                        .AddClaim("battleId", battleId).Encode();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        public bool ValidateToken(string token)
        {
            try
            {
                JwtBuilder.Create().WithAlgorithm(new NoneAlgorithm()).Decode(token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
