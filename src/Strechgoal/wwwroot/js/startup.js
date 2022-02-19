
import { Presentation } from './presentation-class.js'
import { HttpService } from './httpService-class.js'

document.addEventListener("DOMContentLoaded", () => {
    init();
    console.clear();
});

const httpService = new HttpService()

function init() {
    let presentation = new Presentation(httpService);
}


