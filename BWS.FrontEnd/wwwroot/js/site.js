function gerarBadge(renda) {

    // Sem renda → badge padrão
    if (renda === null || renda === undefined || renda === "" || renda === "null") {
        return `<span class="sem-renda">Sem renda</span>`;
    }

    // Converte para número, corrigindo vírgula
    const rendaNumero = Number(String(renda).replace(",", "."));

    // Se não der pra converter ou for zero → Sem renda
    if (isNaN(rendaNumero) || rendaNumero === 0) {
        return `<span class="sem-renda">Renda não informada</span>`;
    }

    // Formata em BRL sem casas decimais
    const formatado = rendaNumero.toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL",
        minimumFractionDigits: 0,
        maximumFractionDigits: 0
    });

    let classe = "";

    if (rendaNumero <= 980) {
        classe = "renda-a";
    } else if (rendaNumero <= 2500) {
        classe = "renda-b";
    } else {
        classe = "renda-c";
    }

    return `<span class="renda-badge ${classe}">${formatado}</span>`;
}
