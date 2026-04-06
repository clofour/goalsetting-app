import { defineConfig } from 'orval';

export default defineConfig({
    goalapp: {
        input: {
            target: 'http://localhost:5076/openapi/v1.json'
        },
        output: {
            mode: 'tags-split',
            client: 'swr',
            target: 'src/api/endpoints',
            schemas: 'src/api/models'
        }
    },
    goalappZod: {
        input: {
            target: 'http://localhost:5076/openapi/v1.json'
        },
        output: {
            mode: 'tags-split',
            client: 'zod',
            target: 'src/api/endpoints'
        }
    },
})