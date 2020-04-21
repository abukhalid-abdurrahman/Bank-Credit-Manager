create table dbo.admin_list_table (
    _id             integer identity(1,1) not null,
    _admin_name     nvarchar(6) not null,
    _admin_password nvarchar(35) not null,
    constraint PK_admin_table_list primary key clustered(ID asc)
);

create table dbo.users_list_table (
    _id                               integer identity(1,1) not null,
    _user_name                        nchar(30) not null,
    _user_password                    nchar(35) not null,
    _user_sex                         nchar(3) not null,
    _user_age                         integer not null,
    _married                          nchar(10) not null,
    _nationality                      nchar(15) not null,
    _credit_summ_from_general_revenue nvarchar(10) not null,
    _credit_history                   nvarchar(15) not null,
    _arrearage_in_credit_history      nvarchar(10) not null,
    _credit_aim                       nvarchar(15)  not null,
    _credit_term                      nvarchar(10) not null,
    _status                           nvarchar(20),
    _balls                            integer,
    _results                          nvarchar(25),
    _is_payed                         integer,
    constraint PK_admin_table_list primary key clustered(ID asc)
);