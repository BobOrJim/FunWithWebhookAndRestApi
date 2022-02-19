
export class HttpService{
    constructor(){
        this.dataFromBackend
    }

    send(){
    }

    recieve(){

        fetch('https://localhost:7288/api/v01/highscores')
        .then( (result) => result.json())
        .then( (data) => this.dataFromBackend = data)
        .catch(error => console.log("NETWORK ERROR, CANT CONNECT TO BACKEND"))
        
        //let result = "[{\"id\":\"3f358291-e79b-4c31-8394-08d9eb561d71\",\"name\":\"jimmy\",\"time\":357},{\"id\":\"240d0a2c-430c-41e3-8395-08d9eb561d71\",\"name\":\"knalle\",\"time\":100},{\"id\":\"bf3f3002-7e53-441e-8b76-f6280be284aa\",\"name\":\"BeatrixKiddo\",\"time\":170}]";
        //this.dataFromBackend = JSON.parse(result)



    }
}


