function changeInputWidth() {

    //Artist Width Change
    var seedIntermediaryWidth = document.getElementById("artist-seeds-container").offsetWidth;
    var root = document.querySelector(':root');
    root.style.setProperty('--artist-seed-intermediary-width', seedIntermediaryWidth + "px");


    //Track Width Change
    var seedIntermediaryWidth = document.getElementById("track-seeds-container").offsetWidth;
    var root = document.querySelector(':root');
    root.style.setProperty('--track-seed-intermediary-width', seedIntermediaryWidth + "px");

    //Genre Width Change
    var seedIntermediaryWidth = document.getElementById("genre-seeds-container").offsetWidth;
    var root = document.querySelector(':root');
    root.style.setProperty('--genre-seed-intermediary-width', seedIntermediaryWidth + "px");
}

function addNewArtist() {

    var input = document.getElementById("artist-input");
    var inputValue = input.value;

    if (inputValue.trim() != "" && counter < 5) {

        var newSpan = createDiv(inputValue);

        var seedIntermediary = document.getElementById("artist-seeds-container");
        seedIntermediary.appendChild(newSpan);

        input.value = "";
        counter++;

        changeInputWidth();

    }
    else {

        seedInputErrors(inputValue);
        input.value = "";
    }

}

function addNewArtistEnter(event) {

    if (event.key == "Enter") {

        document.getElementById("add-artist-bttn").click();

    }
}

function addNewTrack() {

    var input = document.getElementById("track-input");
    var inputValue = input.value;

    if (inputValue.trim() != "" && counter < 5) {

        var newSpan = createDiv(inputValue);

        var elem = document.getElementById("track-seeds-container");
        elem.appendChild(newSpan);

        input.value = "";
        counter++;

        changeInputWidth();
    }
    else {
        seedInputErrors(inputValue);
        input.value = "";
    }

}

function addNewTrackEnter(event) {

    if (event.key == "Enter") {

        document.getElementById("add-track-bttn").click();
    }
}

function addNewGenre() {

    var input = document.getElementById("genre-input");
    var inputValue = input.value;
    var validGenre = dataListValues.includes(inputValue);

    if (inputValue.trim() != "" && counter < 5 && validGenre == true) {

        var newDiv = createDiv(inputValue);

        var elem = document.getElementById("genre-seeds-container");
        elem.appendChild(newDiv);

        removeGenreOption(inputValue);
        input.value = "";
        counter++;

        changeInputWidth();

        return;
    }
    if (validGenre == false) {
        var toastEmptyInput = document.getElementById("genreInputValidation");
        var toast = bootstrap.Toast.getOrCreateInstance(toastEmptyInput);
        toast.show();

        input.value = "";
        return;
    }
    else {
        seedInputErrors(inputValue);
        input.value = "";
    }

}

function addNewGenreEnter(event) {

    if (event.key == "Enter") {

        document.getElementById("add-genre-bttn").click();
    }
}

function createDiv(inputValue) {

    var newDiv = document.createElement("div");
    newDiv.classList.add("js-individual-seed");

    newDiv.innerHTML = `<span>${inputValue}</span><button onclick="removeSeed(event)">x</button>`

    return newDiv;

}

function removeSeed(event) {

    var target = event.target

    var content = target.closest("div");
    var seed = content.getElementsByTagName("span")[0];
    var seedValue = seed.innerHTML;

    if (dataListValues.includes(seedValue)) {

        addGenreBack(seedValue);
    }

    content.remove();
    counter--;
    changeInputWidth();
}

function addGenreBack(seedValue) {
    var dataList = document.getElementById("genres");

    var newOption = document.createElement("option");
    newOption.value = seedValue;

    dataList.appendChild(newOption);

}

function submitSeeds() {

    setArtistSeeds();
    setTrackSeeds();
    setGenreSeeds();

    if (document.getElementById("artist-seeds").value == "" && document.getElementById("track-seeds").value == "" && document.getElementById("genre-seeds").value == "") {
        var toastEmptyInput = document.getElementById("seedSubmitValidation");
        var toast = bootstrap.Toast.getOrCreateInstance(toastEmptyInput);
        toast.show();

        return false;

    }

    if (!validateLimit()) {

        return false;
    }

    else {
        return true;
    }
}

function setArtistSeeds() {
    var seedCont = document.getElementById("artist-seeds-container");
    var spans = seedCont.getElementsByTagName("span");

    var seeds = "";

    for (var span of spans) {

        var txt = span.textContent;

        seeds = seeds + `${txt},`;
    }

    var output = document.getElementById("artist-seeds");
    output.value = seeds;
}

function setTrackSeeds() {

    var seedCont = document.getElementById("track-seeds-container");
    var spans = seedCont.getElementsByTagName("span");

    var seeds = "";

    for (var span of spans) {

        var txt = span.textContent;

        seeds = seeds + `${txt},`;
    }

    var output = document.getElementById("track-seeds");
    output.value = seeds;

}

function setGenreSeeds() {
    var seedCont = document.getElementById("genre-seeds-container");
    var spans = seedCont.getElementsByTagName("span");

    var seeds = "";

    for (var span of spans) {

        var txt = span.textContent;

        seeds = seeds + `${txt},`;
    }

    var output = document.getElementById("genre-seeds");
    output.value = seeds;

}

function removeGenreOption(value) {
    var option = document.getElementById(value);

    option.remove();
}

function getDataListValues() {

    var dataList = document.getElementById("genres");
    var options = dataList.getElementsByTagName("option");

    var optionValues = [];

    var i = 0;

    for (var option of options) {

        optionValues[i] = option.value;

        i++;
    }

    return optionValues;

}

function seedInputErrors(inputValue) {

    if (inputValue.trim() == "") {
        var toastEmptyInput = document.getElementById("toastEmptySeed");
        var toast = bootstrap.Toast.getOrCreateInstance(toastEmptyInput);
        toast.show();
        return;
    }

    if (counter >= 5) {
        var toastEmptyInput = document.getElementById("toastSeedCount");
        var toast = bootstrap.Toast.getOrCreateInstance(toastEmptyInput);
        toast.show();
        return;
    }

    else {
        var toastEmptyInput = document.getElementById("error");
        var toast = bootstrap.Toast.getOrCreateInstance(toastEmptyInput);
        toast.show();
        return;
    }
}

function setLimit() {

    var userValue = document.getElementById("user-limit-input").value;

    if (validateLimit()) {
        var output = document.getElementById("recommendation-limit");
        output.value = userValue;
        return;
    }

}

function validateLimit() {

    var value = document.getElementById("user-limit-input").value;

    var toastInvalidLimit = document.getElementById("toastInvalidLimit");
    var toast = bootstrap.Toast.getOrCreateInstance(toastInvalidLimit);

    if (value <= 100 && value > 0) {
        toast.hide();
        return true;
    }

    else {
        toast.show();
        return false;
    }
}

function showGenerate() {

    var generate = document.getElementById("js-generate");

    if (generate.style.display == "none" || generate.style.display == "") {

        document.getElementById("js-show-generate").style.display = "none";

        document.getElementById("js-hide-generate").style.display = "flex";
        generate.style.display = "initial";
    }

}

function hideGenerate() {

    var generate = document.getElementById("js-generate");

    if (generate.style.display == "initial") {

        document.getElementById("js-hide-generate").style.display = "none";

        document.getElementById("js-show-generate").style.display = "block";

        generate.style.display = "none";
    }


}

function like(e) {

    var container = e.closest("div");
    var track = container.getElementsByClassName("js-track-id")[0];
    var trackId = track.value;

    var likedTrack = recommendedTracks.find(x => x.id === trackId);

    //like
    if (e.classList.contains("bi-heart")) {

        e.classList.toggle("bi-heart");
        e.classList.toggle("bi-heart-fill");

        var uri = 'songrecommendations/addfavorite'

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(likedTrack)
        })

        return;

    }

    //unlike
    if (e.classList.contains("bi-heart-fill")) {

        e.classList.toggle("bi-heart");
        e.classList.toggle("bi-heart-fill");

        var uri = 'songrecommendations/removefavorite'

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(likedTrack)
        })

        return;
    }

}
