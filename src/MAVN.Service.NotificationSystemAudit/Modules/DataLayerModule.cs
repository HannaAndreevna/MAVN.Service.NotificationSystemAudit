using Autofac;
using MAVN.Common.MsSql;
using MAVN.Service.NotificationSystemAudit.Domain.Repositories;
using MAVN.Service.NotificationSystemAudit.MsSqlRepositories;
using MAVN.Service.NotificationSystemAudit.MsSqlRepositories.Repositories;
using MAVN.Service.NotificationSystemAudit.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.NotificationSystemAudit.Modules
{
    public class DataLayerModule : Module
    {
        private readonly string _connectionString;

        public DataLayerModule(IReloadingManager<AppSettings> appSettings)
        {
            _connectionString = appSettings.CurrentValue.NotificationSystemAuditService.Db.DataConnString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMsSql(
                _connectionString,
                connString => new AuditMessageContext(connString, false),
                dbConn => new AuditMessageContext(dbConn));

            builder.RegisterType<AuditMessageRepository>()
                .As<IAuditMessageRepository>()
                .SingleInstance();
        }
    }
}
