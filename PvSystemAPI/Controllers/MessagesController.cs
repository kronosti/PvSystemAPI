using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PvSystemAPI.Models;
using PvSystemAPI.DTOs;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace PvSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly kronosti_cbsContext _context;

        public MessagesController(kronosti_cbsContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        /// <summary>
        /// Get The message from a view from the database, the view is the union of both tables, tbMessage and tbsendedmessage
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VsSendedMessage>>> GetTbMessages()
        {
            return await _context.VsSendedMessages.ToListAsync(); //
        }
                 
        // POST: api/Messages
        /// <summary>
        /// Method to send the message to the database
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TbMessage>> PostTbMessage(MsgDTO message)
        {
            try
            {
                                
                var credential = _context.TbTwilioCredentials.FirstOrDefault(); // Get the default credential to Twilio
                           
                TwilioClient.Init(credential.AccountSid , credential .AuthToken ); // initializing twilio tool with their credentials

                string twilioresponse;
                string[] ToNumbers = message.tomsg .Split(';'); // Split the diferent telephone numbers

                foreach (var toNumber in ToNumbers)
                {
                    if (toNumber != "")
                    {

                        try
                        {
                            // Create a twiliomessa to each number recive
                            //Note:     The Sid recived from twilio work just with the telephone number asociated with the trial version, if i send to diferents 
                            //          number the message is not send, so, i create a secondary twilio account and happend the same, the message sending work just for the numbre asociated with the trial version
                            var twiliomessage = MessageResource.Create( 
                            body: message.messagetxt, //  adding the text message
                            from: new Twilio.Types.PhoneNumber(credential.PhoneNumber), 
                            to: new Twilio.Types.PhoneNumber(toNumber)// specifying  the number
                        );
                            twilioresponse = twiliomessage.Sid; // reciving the Sid from twilio, if the message wasn't send the response is negative and go to de cathc
                        }
                        catch (Exception extw)
                        {
                            twilioresponse = "Invalid phone number";
                        }

                        TbMessage msg = new TbMessage(); // create a object class to save to the datatable 
                        msg.Tomsg = toNumber; // adding values
                        msg.Messagetxt = message.messagetxt;
                        // creating a foreing tabble to the tbmessage to save theconfirmation code from twilio 
                        Tbsendedmessage msgsended = new Tbsendedmessage();
                        msgsended.Confimationcode = twilioresponse;
                        msg.Tbsendedmessages.Add(msgsended);
                        _context.TbMessages.Add(msg); // adding a set of row to each message to number recived
                    }
                    
                }
                 await _context.SaveChangesAsync();  // executing the save data in the database
                return Ok(); // is all go well return OK
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex.InnerException);
            }
          
         
        }

       
    }
}
