export default async function (req) {

    const API_KEY = Deno.env.get("API_KEY");
    const API_URL = Deno.env.get("API_URL");

    const url = new URL(req.url);

    // Remove "/tmdb/" from the beginning of the path
    const endpoint = url.pathname.replace(/^\/tmdb\//i, "");

    const targetUrl =
        `${API_URL.replace(/\/$/, "")}/${endpoint}${url.search}`;

    const response = await fetch(targetUrl, {
        method: req.method,
        headers: {
            Authorization: `Bearer ${API_KEY}`,
            Accept: "application/json"
        }
    });

    return new Response(response.body, {
        status: response.status,
        headers: response.headers
    });
}