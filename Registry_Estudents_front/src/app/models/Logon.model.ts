export class Logon {
    isSucces:Boolean;
    user: User;
    token: string;
}
class User {
    id?:string;
    name: string;
    phone: string;
    mail: string;
    password:string;
}