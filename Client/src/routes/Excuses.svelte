<script lang="ts">
  	let excuses = [
		{ id: 1, text: `Krankheit`, value: "ich krank bin" },
		{ id: 2, text: `Kinderkrankheit`, value: 'mein Kind krank ist' },
		{ id: 3, text: `Amtlichen Termin`, value: 'einen Amtlichen Termin habe' }
	];

  let lateMessages = [
		{ id: 0, text: ``, value: "" },
		{ id: 1, text: `Einfach`, value: 'Ich verspäte mich' },
		{ id: 2, text: `S-Bahn`, value: 'Ich verspäte mich, weil meine S-Bahn ausfällt' },
		{ id: 3, text: `Termin`, value: 'Ich verspäte mich, ich bin gerade von einem Termin zurückgekommen' }
	];

  let day: string = $state(new Date().toISOString().split('T')[0]);
  let excuse: {id: number, text: string, value: string } = $state(excuses[0]);
  let lateMsg: {id: number, text: string, value: string } = $state(lateMessages[0]);

  let fname=$state(localStorage['fname']);
  let lname=$state(localStorage['lname']);
  let email = $state('');
  let emailbody = $state('');
  let emailsubj = $state('');
  let wa = $state('');

  function buildTexts(){
    localStorage['fname'] = fname;
    localStorage['lname'] = lname;

    email = `Hallo,
ich bin ${fname||'name'} ${lname||'nachname'} und ich kann am ${dateToString(new Date(day))} leider nicht zum Unterricht kommen, weil ${excuse.value }.`;
    emailsubj = `B2 BSK Nr.70 ${fname||'name'} ${lname||'nachname'} Entschuldigung ${dateToString(new Date(day))}`
    emailbody = email.replaceAll("\n","%0D%0A");
    if(lateMsg.id > 0){
      wa = lateMsg.value;
    }else{
      wa  = `Ih komme Heute nicht, weil ${excuse.value}`;
    }
  }

  function dateToString(d:Date){
    return `${d.getDate()}.${d.getMonth()+1}.${d.getFullYear()}`
  }

  function copyWa(){
    if('share' in navigator){
      navigator.share({ text: wa });
      
    } else {
      navigator.clipboard.writeText(wa);
    }
  }

  buildTexts();
</script>

<p>Wenn man nicht zum Kurs kommen kann, muss man eine Entschuldigungs-E-Mail an <a href="mailto:sprachinstitut-muenchen@ib.de" class="link" target="_blank">sprachinstitut-muenchen@ib.de</a> schicken und eine kurze Nachricht in die WhatsApp-Gruppe schreiben.
Dafür kann man eine der Vorlagen nutzen.</p>

<form class="border border-1 p-3 bg-primary text-white fw-bold border-black" autocomplete="on">
  <div class="row" >
      <label for="name" class="col-sm-2 col-form-label">Vorname</label>
      <div class="col-md-4">
        <input type="text" class="form-control" id="name"  name="first_name" bind:value={fname} oninput="{buildTexts}">
      </div>
      <label for="lastname" class="col-sm-2 col-form-label">Nachname</label>
      <div class="col-md-4">
        <input type="text" class="form-control" id="lastname" name="last_name" bind:value={lname} oninput="{buildTexts}">
      </div>
      <div class="mt-2 w-100"></div>
      <label for="excuse" class="col-sm-2 col-form-label">Entschuldigung</label>
      <div class="col-md-4">
        <select bind:value={excuse} onchange={buildTexts} class="form-select" id="excuse">
          {#each excuses as e}
            <option value={e}>
              {e.text}
            </option>
          {/each}
        </select>
      </div>
      
      <label for="date" class="col-sm-2 col-form-label">Date</label>
      <div class="col-md-4">
        <input type="date" class="form-control" id="date" bind:value={day} oninput="{buildTexts}">
      </div>

      <div class="mt-2 w-100"></div>

      <label for="lmess" class="col-sm-2 col-form-label">Ausrede</label>
      <div class="col-md-4">
        <select bind:value={lateMsg} onchange={buildTexts} class="form-select" id="lmess">
          {#each lateMessages as e}
            <option value={e}>
              {e.text}
            </option>
          {/each}
        </select>
      </div>

    </div>
  </form>

  <div class="text-white bg-info p-3 mt-3">
    <div class="row">
      <div class="col-md-10">
        <p class="h4">E-mail Betreff</p>
        <p>{emailsubj}</p>
        <p class="h4">E-mail</p>
        <div class="col-md-10">
          <pre class="text-wrap">{email}</pre>
        </div>
        <p class="h4">WhatsApp-Nachricht</p>
        <p>{wa}</p>
      </div>
      <div class="col-md-2 btn-group-vertical justify-content-end">
        <a type="button" class="btn btn-primary flex-grow-0" href={`mailto:sprachinstitut-muenchen@ib.de?subject=${emailsubj}&body=${emailbody}`}>Email ofnen</a>
        <button type="button" class="btn btn-secondary mt-2 flex-grow-0" onclick={copyWa}>WhatsApp-Nachricht Kopieren</button>
      </div>
    </div>
  </div>