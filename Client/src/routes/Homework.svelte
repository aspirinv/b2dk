<script lang="ts">
    import { onMount } from 'svelte';

    let data = $state([]);
    onMount(async () => {
        const response = await fetch(`/api/halist`);
        const rsp = await response.json();
        rsp.forEach(e=>e.done = localStorage["task_" + e.id] === "true");
        data=rsp;
    });

interface task{
  id:string;
  description: string;
  type: string;
  done: boolean;
  till: string;
}
</script>

<div>
  {#each data as t}
    <div class="d-flex border border-primary {t.id%2 === 0? 'bg-info-subtle':'bg-warning-subtle'}">
      {#if t.type.toLowerCase() === 'b'}
        <i class="bi bi-journal-text"></i>
        {:else}
        <i class="bi bi-file-earmark"></i>
      {/if}
      <label for="{t.id}" class="form-label ms-2 flex-fill">{t.description}</label>
      <p class="my-1 mx-2">{t.till}</p>
      <input type="checkbox" class="form-check-input me-2 mt-2" id="{t.id}" onchange="{(s)=>  localStorage["task_" + t.id] = s.target.checked}" bind:checked={t.done}>
    </div>
  {/each}
</div>
