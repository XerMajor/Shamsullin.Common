﻿using System.Configuration;
using log4net.Core;
using Telegram.Bot;

namespace log4net.Appender.Telegram
{
    public class TelegramAppender : AppenderSkeleton
    {
        public string Token { get; set; }

        public string ChatId { get; set; }
        
        protected TelegramBotClient Bot;
        
        public TelegramAppender()
        {
            Bot = new TelegramBotClient(Token);
        }

        protected override void Append(LoggingEvent e)
        {
            if (string.IsNullOrEmpty(Token))
            {
                throw new ConfigurationErrorsException("Please set the Token under TelegramAppender configuration section: <Token>...</Token>");
            }

            if (string.IsNullOrEmpty(ChatId))
            {
                throw new ConfigurationErrorsException("Please set the ChatId under TelegramAppender configuration section: <ChatId>...</ChatId>");
            }

            var message = Layout == null ? e.RenderedMessage : RenderLoggingEvent(e);
            Bot.SendTextMessageAsync(ChatId, message);
        }
    }
}
