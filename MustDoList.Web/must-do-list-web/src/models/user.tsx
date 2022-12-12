export class User {

    constructor(id: number, name: string, email: string, refreshToken: string, bearerToken: string) {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.RefreshToken = refreshToken;
        this.BearerToken = bearerToken;
    }

    Id: number = 0;
    Name: string = "";
    Email: string = "";
    RefreshToken: string = "";
    BearerToken: string = "";
}