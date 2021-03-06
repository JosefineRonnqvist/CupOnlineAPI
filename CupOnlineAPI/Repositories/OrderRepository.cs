using CupOnlineAPI.Context;
using CupOnlineAPI.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using NETCore.MailKit.Core;

namespace CupOnlineAPI.Repositories
{
    public class OrderRepository
    {
        private readonly DapperContext _context;
        public IEmailService _emailService { get; set; }

        public OrderRepository(DapperContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        /// <summary>
        /// Get organizer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Organizer> GetOrganizerById(int id)
        {
            var query = @"SELECT club_id, club_name FROM td_clubs
                        WHERE club_id=@club_id";
            using (var connection = _context.CreateConnection())
            {

                var organizer = await connection.QueryFirstAsync<Organizer>(query, new
                {
                    club_id=id
                });
                return organizer;
            }
        }

        /// <summary>
        /// Get all registered organizers
        /// </summary>
        /// <returns>List of organizers with name and id</returns>
        public async Task<IEnumerable<Organizer>> GetAllOrganizers()
        {
            var query = @"SELECT club_id, club_name from td_clubs 
                        ORDER BY club_name ASC";
            using (var connection = _context.CreateConnection())
            {
                var cups = await connection.QueryAsync<Organizer>(query);
                return cups.ToList();
            }
        }

        /// <summary>
        /// Get all registered sports
        /// </summary>
        /// <returns>List of sports with name and id</returns>
        public IEnumerable<Sport> GetAllSports()
        {
            using (var connection = _context.CreateConnection())
            {
                return connection.GetAll<Sport>().OrderBy(s=>s.sport_name);
            }
        }

        /// <summary>
        /// Create new city
        /// </summary>
        /// <param name="city">City to insert in table</param>
        /// <returns>Created city id</returns>
        public async Task<int> CreateCity(City city)
        {
            using (var connection = _context.CreateConnection())
            {
                var newCity = new City
                {
                    city_name = city.city_name,
                };
                return await connection.InsertAsync(newCity);
            }
        }

        /// <summary>
        /// Creates a new organizer
        /// </summary>
        /// <param name="organizer">organizer to create</param>
        /// <returns>Created organizer id</returns>
        public async Task<int> CreateOrganizer(Organizer organizer)
        {
            using (var connection = _context.CreateConnection())
            {
                var newOrganizer = new Organizer
                {
                    club_name = organizer.club_name,
                    club_shortname = organizer.club_shortname,
                    club_url = organizer.club_url,
                    club_city_id = organizer.club_city_id,
                    club_sport_id = organizer.club_sport_id,
                    club_status = organizer.club_status,
                };
                return await connection.InsertAsync(newOrganizer);
            }
        }

        /// <summary>
        /// create a new cup
        /// </summary>
        /// <param name="cup">Details about cup from form</param>
        /// <returns>Created cup id</returns>
        public async Task<int> CreateCup(OrderCup cup)
        {
            using (var connection = _context.CreateConnection())
            {
                var newCup = new OrderCup
                {
                    cup_club_id = cup.cup_club_id,
                    cup_sport_id = cup.cup_sport_id,
                    cup_logotype= cup.cup_logotype,
                    cup_sponsor_logotype = cup.cup_sponsor_logotype,
                    cup_sponsor_url = cup.cup_sponsor_url,
                    cup_url=cup.cup_url,
                    cup_date = cup.cup_date,
                    cup_startdate = cup.cup_startdate,
                    cup_enddate = cup.cup_enddate,
                    cup_name= cup.cup_name,
                    cup_players_age = cup.cup_players_age,
                    cup_groups=cup.cup_groups,
                    cup_periods=cup.cup_periods,
                    cup_periodtime=cup.cup_periodtime,
                    cup_play_place= cup.cup_play_place,
                    cup_round=cup.cup_round,
                    cup_game_no=cup.cup_game_no,
                    cup_table_sort=cup.cup_table_sort,
                    cup_show_teammembers=cup.cup_show_teammembers,
                    cup_game_report=cup.cup_game_report,
                    cup_sponsors=cup.cup_sponsors,
                    cup_status=cup.cup_status,
                    cup_binStatus=cup.cup_binStatus,
                    cup_gamewin_points=cup.cup_gamewin_points,
                    cup_gamedraw_points=cup.cup_gamedraw_points,
                    cup_gamewinsd_points=cup.cup_gamewinsd_points,
                    cup_gamewinpenalties_points=cup.cup_gamewinpenalties_points,
                    cup_gamewinextra_points=cup.cup_gamewinextra_points
                };
                return await connection.InsertAsync(newCup);
            }
        }

        public async Task<int> CreateCupRegistration(OrderRegistration reg)
        {
            using (var connection = _context.CreateConnection())
            {
                var newOrderReg = new OrderRegistration
                {
                    cup_id = reg.cup_id,
                    message = reg.message,
                    invoiceAddress = reg.invoiceAddress,
                    registrationDate = reg.registrationDate,
                    payDate = reg.payDate,
                    orderStatus = reg.orderStatus,
                    foundType = reg.foundType,
                    regIp = reg.regIp,
                    status = reg.status,
                    payAmount = reg.payAmount,
                };
                return await connection.InsertAsync(newOrderReg);
            }
        }

        public async Task<int> CreateCupAdmin(OrderCupAdmin admin)
        {
            using (var connection = _context.CreateConnection())
            {
                var newAdmin = new OrderCupAdmin
                {
                    cup_user_username = admin.cup_user_username,
                    cup_user_password = admin.cup_user_password,
                    cup_user_cup_id = admin.cup_user_cup_id,
                    cup_user_rights = admin.cup_user_rights,
                    cup_user_name = admin.cup_user_name,
                    cup_user_email = admin.cup_user_email,
                    cup_user_phone = admin.cup_user_phone,
                };
                return await connection.InsertAsync(newAdmin);
            }
        }

        public async Task SendConfirmationMailSe(ConfirmationMailDetails con)
        {
            var subject = "CupOnline användaruppgifter -" + con.cup_name;
            var body = $"Hej, <br><br>Tack för att ni väljer att använda CupOnline, här kommer era användaruppgifter.<br><br>" +
                           $"Adress: <a href=\"https://www.CupOnline.se/admin_login.aspx?cupid= {con.cup_id}\">https://www.CupOnline.se/admin_login.aspx?cupid= {con.cup_id}" +
                           $"</a><br><br>Användarnamn: {con.cup_user_username}<br>" +
                           $"Lösenord: {con.cup_user_password}<br><br>Support<br>Använd i första hand&nbsp;formuläret som finns länkat från administrationssidan.<br>" +
                           $"Mail: {con.fromMail}<br><br>Om ni vänder er till deltagare under U13 får ni nyttja CupOnline kostnadsfritt så länge ni behåller " +
                           $"CoreIT som huvudsponsor (länk + logo).<br><br><strong>Betalningsrutiner<br></strong>Avgiftsbelagd cup betalas innan den kan göras publik, " +
                           $"men du kan börja jobba med innehåll innan dess.<br>Betalning&nbsp;görs via CupOnline efter att man har loggat in.<br>" +
                           $"Tillgängliga betalsätt är betalkort, internetbank eller faktura.<br><br>Mvh<br>CupOnline<br>www.cuponline.se<br><br>";
            bool isHtml = true;
            await _emailService.SendAsync(con.toMail, subject, body, isHtml);
        }

        public async Task SendConfirmationMailEn(ConfirmationMailDetails con)
        {
            var subject = "CupOnline login information -" + con.cup_name;
            var body = $"Hi, <br><br>Thank you for choosing CupOnline, here is your login information.<br><br>" +
                           $"Address: <a href=\"https://www.CupOnline.se/admin_login.aspx?cupid= {con.cup_id}\">https://www.CupOnline.se/admin_login.aspx?cupid= {con.cup_id}" +
                           $"</a><br><br>User name: {con.cup_user_username}<br>" +
                           $"Password: {con.cup_user_password}<br><br>Support<br>Please use the form that is available&nbsp;on the administration page.<br>" +
                           $"Mail: {con.fromMail}<br><br><strong>New payment procedures<br></strong>On tournaments that requires payment&nbsp;" +
                           $"you have to pay before the cup can be made public. You can still start with the content ahead of paying and publishing." +
                           $"<br><br>Regards<br>CupOnline<br>www.cuponline.se<br><br>";
            bool isHtml = true;
            await _emailService.SendAsync(con.toMail, subject, body, isHtml);
        }

        public async Task SendOrderMail(OrderMailDetails order)
        {
            var subject = "CupOnline ny cup -" + order.cup_name;
            var body = $"<p>Ny cup registrerad: <strong>{order.cup_name}</strong><br><br><strong>Meddelande</strong><br>{order.message}<br><br><strong>Arrangör</strong>" +
                $"<br>{order.organizer}<br><br><strong>Cupinställningar</strong><br>Namn: {order.cup_name}<br>Sport: {order.sport}<br>Ålder: {order.cup_players_age}<br>" +
                $"Spelplats: {order.cup_play_place}<br>Datum: {order.cup_startdate} till {order.cup_enddate}<br><br><strong>Kontakt</strong><br>Namn: {order.cup_user_name}<br>" +
                $"Telefon: {order.cup_user_phone}<br>E-post:&nbsp;{order.cup_user_email}<br><br><strong>Faktureringsadress</strong><br>{order.invoiceAddress}" +
                $"<br><br><strong>CupOnline-partners får ta del av adressuppgifter<br><strong>{order.acceptSharing}<br><br><strong>" +
                $"Inloggning</strong><br><br><a href=\"https://www.CupOnline.se/admin_login.aspx?cupid= {order.cup_id}\">https://www.CupOnline.se/admin_login.aspx?cupid= {order.cup_id}</a>" +
                $"</p><p><a href=\"http://www.CupOnline.se/admin_login.asp?cupid= {order.cup_id}\"></a><br>Användarnamn: {order.cup_user_username}<br>Lösenord: {order.cup_user_password}";
            bool isHtml = true;
            await _emailService.SendAsync(order.toMail, subject, body, isHtml); ;
        }
    }
}
