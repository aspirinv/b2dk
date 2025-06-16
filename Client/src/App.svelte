<script lang="ts">

  import { Router, Link, Route } from "svelte-routing";

  import Home from "./routes/Home.svelte";
  import Excuses from "./routes/Excuses.svelte";
  import Homework from "./routes/Homework.svelte";

  export let url = "";

</script>

<Router {url}>
    <nav class="navbar navbar-expand-lg bg-primary" data-bs-theme="dark">
        <div class="container-fluid">
            <Link to="/" class="navbar-brand" >B2 BSK 70</Link>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarColor01">
            <ul class="navbar-nav me-auto">
                <li class="nav-item">
                    <Link to="/excuses" class="nav-link" >Ausreden</Link>
                </li>
                <li class="nav-item">
                    <Link to="/homework" class="nav-link" >Hausaufgaben</Link>
                </li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="border-bottom mb-5"></div>
    <div class="container mb-auto">
        <Route path="/">
            <Home />
        </Route> 
         <Route path="/excuses">
            <Excuses />
        </Route>
         <Route path="/homework">
            <Homework />
        </Route>
    </div>
</Router>
